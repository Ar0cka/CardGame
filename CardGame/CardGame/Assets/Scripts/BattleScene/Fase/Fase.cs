using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Fase : MonoBehaviour
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

   [SerializeField] private DropCardInPanel _zoneCards;
   [SerializeField] private HandCards _handCards;

   private HandlerController _handlerController;
   private bool isFirstTurn;
   
   public void BeginBuildPhase()
   {
      if (!isFirstTurn)
      {
         foreach (var vCard in cardsHand)
         {
            _handlerController = vCard.GetComponent<HandlerController>();
            _handlerController.OnHandlersFromHand();
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
      FirstRepackList();
      SecondRepackList();
      
      _zoneCards.ResetCountHandler();
      
      permutationPhase = "Permutation phase";
      
      ChangePhase(ref beginBuildPhase, ref beginPenutationFase, "End permutation phase", permutationPhase);
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
            _handlerController.OnSwitchHandler();
         }
      }
   }

   private void SecondRepackList()
   {
      _handCards.repackHandList(cardsHand);
      foreach (var vCard in cardsHand)
      {
         _handlerController = vCard.GetComponent<HandlerController>();
         _handlerController.OffHandlersFromHand();
      }
   }

   #endregion
   
   public void BeginBattle()
   {
      foreach (var vCard in cardsZone)
      {
         _handlerController = vCard.GetComponent<HandlerController>();
         _handlerController.OffSwitchHandler();
         _handlerController.OnAttackHandler();
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
         _handlerController.OffAttackHandler();
      }

      attackPhase = "Attack phase";
      ChangePhase(ref _beginBattleFase, ref _beginAttackPhase, "End attack phase", attackPhase);
   }
   
   public void EndPhase()
   {
      _endPhase = "End phase";
      ChangePhase(ref _beginAttackPhase, ref _beginEndPhase, "End turn", _endPhase);
      isFirstTurn = false;
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
