using System;
using System.Collections;
using System.Collections.Generic;
using CardSettings.CardPrefabSettings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerPhase : MonoBehaviour
{
   private string _buildPhase, _permutationPhase, _battlePhase, _attackPhase, _endPhase;
   
   private bool _beginBuildPhase, _beginPermutationPhase, _beginBattlePhase, _beginAttackPhase, _beginEndPhase;

   private bool _monster, _player;

   public bool IsBuildPhase => _beginBuildPhase;
   public bool IsPermutationPhase => _beginPermutationPhase;
   public bool IsBattlePhase => _beginBattlePhase;
   public bool IsEndPhase => _beginEndPhase;
   public bool IsAttackPhase => _beginAttackPhase;

   List<CardPrefab> _cardsZone = new List<CardPrefab>();
   List<CardPrefab> _cardsHand = new List<CardPrefab>();

   [SerializeField] private TextMeshProUGUI _phaseTextGUI;
   [SerializeField] private TextMeshProUGUI _buttonsEndPhase;

   [SerializeField] private CardZoneController _zoneCards;
   [SerializeField] private HandCards _handCards;
   
   private TokenEffectOnOpponent _tokenEffectOnOpponent = TokenEffectOnOpponent.Instance;
   private TokenEffectOnFriendlyCards _tokenEffectOnFriendlyCards = TokenEffectOnFriendlyCards.Instance;

   private EnemyBattlePhase _enemyBattle;
   private HandlerController _handlerController;
   private bool isFirstTurn;

   private void Awake()
   {
      _enemyBattle = FindObjectOfType<EnemyBattlePhase>();
   }

   public void BeginBuildPhase()
   {
      if (!isFirstTurn)
      {
         foreach (var vCard in _cardsHand)
         {
            _handlerController = vCard.GetComponent<HandlerController>();
            _handlerController.SettingsHandlersFromHand(IsBuildPhase);
         }
         _cardsZone.Clear();
      }
      _buildPhase = "Build phase";
      #region BuildFase

      if (_beginEndPhase)
      {
         ChangePhase(ref _beginEndPhase, ref _beginBuildPhase, "End build phase", _buildPhase);
      }
      else
      {
         ChangePhase(ref _beginBuildPhase, "End build phase", _buildPhase);
      }
      #endregion
   }

   public void BeginPenutationPhase()
   {
      _permutationPhase = "Permutation phase";
      ChangePhase(ref _beginBuildPhase, ref _beginPermutationPhase, "End permutation phase", _permutationPhase);
      
      FirstRepackList();
      SecondRepackList();
      
      _zoneCards.ResetCountHandler();
   }
   
   #region RepackListsInPenutationPhase

   private void FirstRepackList()
   {
      _zoneCards.repackAllLists(_cardsZone);
      foreach (var vCard in _cardsZone)
      {
         if (vCard._cardInfo.subtype != CardInfo.SubtypeCard.AuxiliaryBuild &&
             vCard._cardInfo.subtype != CardInfo.SubtypeCard.AttackRangeBuild)
         {
            _handlerController = vCard.GetComponent<HandlerController>();

            switch (vCard.isBattleZone)
            {
               case false:
                  _handlerController.OnSwitchHandler();
                  break;
               case true:
                  _handlerController.OnHandlerFromBattleZone();
                  break;
            }
         }
      }
   }

   private void SecondRepackList()
   {
      _handCards.repackHandList(_cardsHand);
      foreach (var vCard in _cardsHand)
      {
         _handlerController = vCard.GetComponent<HandlerController>();
         _handlerController.SettingsHandlersFromHand(IsBuildPhase);
      }
   }

   #endregion
   
   public void BeginBattle()
   {
      foreach (var vCard in _cardsZone)
      {
         _handlerController = vCard.GetComponent<HandlerController>();
         _handlerController.BeginBattleFase();
      }
      #region ChangeFase
      _battlePhase = "Battle phase";
      ChangePhase(ref _beginPermutationPhase, ref _beginBattlePhase, "End battle phase", _battlePhase);
      #endregion
   }

   public void BeginAttackPhase()
   {
      foreach (var vCard in _cardsZone)
      {
         _handlerController = vCard.GetComponent<HandlerController>();
         _handlerController.BeginAttackPhase();
      }

      _attackPhase = "Attack phase";
      ChangePhase(ref _beginBattlePhase, ref _beginAttackPhase, "End attack phase", _attackPhase);
      StartCoroutine(DelayAttackUnits()); 
      
      EndPhase();
   }

   private IEnumerator DelayAttackUnits()
   {
      yield return new WaitForSeconds(0f);
      AssigningAttackers.Attack(_tokenEffectOnOpponent);
   }
   
   public void EndPhase()
   {
      _endPhase = "End phase";
      ChangePhase(ref _beginAttackPhase, ref _beginEndPhase, "End turn", _endPhase);
      DeleteTokenFromFriendlyCard(_cardsZone);
      isFirstTurn = false;
   }

   public void BeginEnemyTurn()
   {
      _beginEndPhase = false;
      
      ClearIsDeleted();
      DealDamageFromTokensTheEndTurn();
   }

   private void DealDamageFromTokensTheEndTurn()
   {
      if (_tokenEffectOnOpponent._deathTokenInEnemy.Count > 0)
      {
         _tokenEffectOnOpponent.DealDamageFromDeathToken();
      }
   }

   private void ClearIsDeleted()
   {
      foreach (var card in _cardsZone)
      {
         if (card.isDeletedTokenFromCard)
         {
            card.ClearIsDeletedTokenFromCard();
         }
      }
   }
   
   private void DeleteTokenFromFriendlyCard(List<CardPrefab> _cardsFromBattleZone)
   {
      foreach (var card in _cardsFromBattleZone)
      {
         var tokenSystem = card.GetComponent<TokenInCardSystem>();

         if (_tokenEffectOnFriendlyCards.fearTokens.Count > 0)
         {
            try
            {
               if (tokenSystem.canAttack == false)
               {
                  _tokenEffectOnFriendlyCards.RemoveFearTokensFromTarget(card);
               }
            }
            catch (Exception e)
            {
               Debug.LogError(e);
            }
         }
      }
   }
   
   #region ChangePhase

   public void ChangePhase(ref bool endPhase, ref bool beginPhase, string buttonText, string PhaseText)
   {
      _phaseTextGUI.text = PhaseText;
      endPhase = false;
      beginPhase = true;
      _buttonsEndPhase.text = buttonText;
   }

   public void ChangePhase(ref bool beginPhase, string buttonText, string PhaseText)
   {
      _phaseTextGUI.text = PhaseText;
      beginPhase = true;
      _buttonsEndPhase.text = buttonText;
   }
   #endregion
}
