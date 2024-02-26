using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Fase : MonoBehaviour
{
   private string buildPhase, permutationPhase, battlePhase, endPhase;
   
   private bool beginBuildPhase, beginPenutationFase, _beginBattleFase, _beginEndPhase;

   private bool Monster, Player;

   public bool _isBuildPhase => beginBuildPhase;
   public bool _isPenutationPhase => beginPenutationFase;
   public bool _isBattlePhase => _beginBattleFase;
   public bool _isEndPhase => _beginEndPhase;

   List<CardPrefab> cardsZone = new List<CardPrefab>();
   List<CardPrefab> cardsHand = new List<CardPrefab>();

   [SerializeField] private TextMeshProUGUI _phaseText, _buttonsEndPhase;

   [SerializeField] private DropCardInPanel _zoneCards;
   [SerializeField] private HandCards _handCards;

   private HendlerController _hendlerController;
   private bool isFirstTurn;
   
   public void BeginBuildPhase()
   {
      if (!isFirstTurn)
      {
         foreach (var vCard in cardsHand)
         {
            _hendlerController = vCard.GetComponent<HendlerController>();
            _hendlerController.OnHendlersFromHand();
         }
         cardsZone.Clear();
      }
      buildPhase = "Build phase";
      beginBuildPhase = true;
      _phaseText.text = buildPhase;
      _buttonsEndPhase.text = "End build phase";
   }

   public void BeginPenutationPhase()
   {
      FirstRepackList();
      SecondRepackList();
      // тут должно быть выключено перетаскивание с руки, но при этом включино перетаскивание с одной панели на поле в другую
      permutationPhase = "Permutation phase";
      beginBuildPhase = false;
      beginPenutationFase = true;
      _phaseText.text = permutationPhase;
      _buttonsEndPhase.text = "End permutation phase";

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
            _hendlerController = vCard.GetComponent<HendlerController>();
            _hendlerController.PenutationPhase();
         }
      }
   }

   private void SecondRepackList()
   {
      _handCards.repackHandList(cardsHand);
      foreach (var vCard in cardsHand)
      {
         _hendlerController = vCard.GetComponent<HendlerController>();
         _hendlerController.OffHendlersFromHand();
      }
   }

   #endregion
   
   public void BeginBattle()
   {
      foreach (var vCard in cardsZone)
      {
         _hendlerController = vCard.GetComponent<HendlerController>();
         _hendlerController.endPenutationPhase();
      }
      #region ChangeFase
      battlePhase = "Battle phase";
      beginPenutationFase = false;
      _beginBattleFase = true;
      _phaseText.text = battlePhase;
      _buttonsEndPhase.text = "End battle phase";
      #endregion
   }

   public void EndPhase()
   {
      // момент сброса карт с руки и передача хода противнику
      endPhase = "End phase";
      _beginBattleFase = false;
      _beginEndPhase = true;
      _phaseText.text = endPhase;
      _buttonsEndPhase.text = "End turn";
      isFirstTurn = false;
   }
}
