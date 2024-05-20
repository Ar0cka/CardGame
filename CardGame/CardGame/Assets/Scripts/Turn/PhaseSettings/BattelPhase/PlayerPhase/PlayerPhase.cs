using System;
using System.Collections;
using System.Collections.Generic;
using CardSettings.Tokens;
using TMPro;
using Turn.PhaseSettings.BattelPhase;
using Turn.PhaseSettings.BattelPhase.PlayerPhase;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Zenject.SpaceFighter;

public class PlayerPhase : MonoBehaviour, IRepackLists, IBeginAttackUnits
{
   [SerializeField] private CardZoneController _zoneCards;
   [SerializeField] private TurnController _turnController;
   [SerializeField] private EnemyPhase _enemyPhase;
   [SerializeField] private HandCards _handCards;
   
   private List<CardPrefab> cardsZone = new List<CardPrefab>();
   private List<CardPrefab> cardsHand = new List<CardPrefab>();

   private HandlerController _handlerController;

   [Inject] private AssigningAttackers _attack;
   [Inject] private PlayerPhaseSettings _playerPhaseSettings;

   private IRepackLists _repackLists;
   private IBeginAttackUnits _beginAttackUnits;
   
   #region RepackListsInPenutationPhase

   private void Awake()
   {
      _repackLists = GetComponent<PlayerPhase>();
      _beginAttackUnits = GetComponent<PlayerPhase>();
   }

   public void FirstRepackList()
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

   public void SecondRepackList(bool _isBuildPhase)
   {
      _handCards.repackHandList(cardsHand);
      foreach (var vCard in cardsHand)
      {
         _handlerController = vCard.GetComponent<HandlerController>();
         _handlerController.SettingsHandlersFromHand(_isBuildPhase);
      }
   }

   #endregion

   public void BeginAttack()
   {
      StartCoroutine(DelayAttackUnits());
   }
   
   private IEnumerator DelayAttackUnits()
   {
      yield return new WaitForSeconds(0f);
      _attack.Attack();
   }
   
   public void TurnControllerPlayer()
   {
      if (_playerPhaseSettings._isBuildPhase)
      {
         _playerPhaseSettings.BeginPenutationPhase(_beginAttackUnits, _repackLists, _zoneCards);
      }
      else if (_playerPhaseSettings._isPenutationPhase)
      {
         _playerPhaseSettings.BeginBattle(cardsZone);
      }
      else if (_playerPhaseSettings._isBattlePhase)
      {
         _playerPhaseSettings.BeginAttackPhase(cardsZone);
      }
      else if (_playerPhaseSettings._isEndPhase)
      {
         _playerPhaseSettings.BeginEnemyTurn();
         _turnController.TurnEnemy();
      }
      else if (_enemyPhase._isAssignDefense)
      {
         _enemyPhase.AttackEnemyPhase(); 
      }
   } // начало фазы перестановки
   
}
