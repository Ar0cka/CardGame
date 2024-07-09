using System;
using System.Collections;
using System.Collections.Generic;
using CardSettings.CardPrefabSettings;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerPhase : MonoBehaviour
{
   private string buildPhase, permutationPhase, battlePhase, attackPhase, _endPhase;
   
   private bool beginBuildPhase, beginPenutationFase, _beginBattleFase, _beginAttackPhase, _beginEndPhase;

   private bool Monster, Player;

   public bool _isBuildPhase => beginBuildPhase;
   public bool _isPenutationPhase => beginPenutationFase;
   public bool _isBattlePhase => _beginBattleFase;
   public bool _isEndPhase => _beginEndPhase;
   public bool _isAttackPhase => _beginAttackPhase;

   List<CardPrefab> cardsZone = new List<CardPrefab>();
   List<CardPrefab> cardsHand = new List<CardPrefab>();

   [FormerlySerializedAs("_phaseText")] [SerializeField] private TextMeshProUGUI _phaseTextGUI;
   [SerializeField] private TextMeshProUGUI _buttonsEndPhase;

   [SerializeField] private CardZoneController _zoneCards;
   [SerializeField] private HandCards _handCards;
   
   private TokenEffectOnOpponent _tokenEffectOnOpponent = TokenEffectOnOpponent.Instance;

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
         foreach (var vCard in cardsHand)
         {
            _handlerController = vCard.GetComponent<HandlerController>();
            _handlerController.SettingsHandlersFromHand(_isBuildPhase);
         }
         cardsZone.Clear();
      }
      buildPhase = "Build phase";
      #region BuildFase

      if (_beginEndPhase)
      {
         ChangePhase(ref _beginEndPhase, ref beginBuildPhase, "End build phase", buildPhase);
      }
      else
      {
         ChangePhase(ref beginBuildPhase, "End build phase", buildPhase);
      }
      #endregion
   }

   public void BeginPenutationPhase()
   {
      permutationPhase = "Permutation phase";
      ChangePhase(ref beginBuildPhase, ref beginPenutationFase, "End permutation phase", permutationPhase);
      
      FirstRepackList();
      SecondRepackList();
      
      _zoneCards.ResetCountHandler();
   }
   
   #region RepackListsInPenutationPhase

   private void FirstRepackList()
   {
      _zoneCards.repackAllLists(cardsZone);
      foreach (var vCard in cardsZone)
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
      _handCards.repackHandList(cardsHand);
      foreach (var vCard in cardsHand)
      {
         _handlerController = vCard.GetComponent<HandlerController>();
         _handlerController.SettingsHandlersFromHand(_isBuildPhase);
      }
   }

   #endregion
   
   public void BeginBattle()
   {
      foreach (var vCard in cardsZone)
      {
         _handlerController = vCard.GetComponent<HandlerController>();
         _handlerController.BeginBattleFase();
      }
      #region ChangeFase
      battlePhase = "Battle phase";
      ChangePhase(ref beginPenutationFase, ref _beginBattleFase, "End battle phase", battlePhase);
      #endregion
   }

   public void BeginAttackPhase()
   {
      foreach (var vCard in cardsZone)
      {
         _handlerController = vCard.GetComponent<HandlerController>();
         _handlerController.BeginAttackPhase();
      }

      attackPhase = "Attack phase";
      ChangePhase(ref _beginBattleFase, ref _beginAttackPhase, "End attack phase", attackPhase);
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
      isFirstTurn = false;
   }

   public void BeginEnemyTurn()
   {
      _beginEndPhase = false;
     
      DealDamageFromTokensTheEndTurn();
   }

   private void DealDamageFromTokensTheEndTurn()
   {
      if (_tokenEffectOnOpponent._deathTokenInEnemy.Count > 0)
      {
         _tokenEffectOnOpponent.DealDamageFromDeathToken();
      }
   }

   private void DeleteTokenFromFriendlyCard(List<CardPrefab> _cardsFromBattleZone)
   {
      
      
      foreach (var card in _cardsFromBattleZone)
      {
         var tokenSystem = card.GetComponent<TokenInCardSystem>();

         if (tokenSystem.canAttack == true)
         {
            
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
