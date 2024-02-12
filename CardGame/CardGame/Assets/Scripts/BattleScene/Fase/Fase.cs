using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

   [SerializeField] private TextMeshProUGUI _phaseText, _buttonsEndPhase;

   [SerializeField ]private CheckEnemyType _checkEnemy;
   
   public void BeginBuildPhase()
   {
      // Тут мы должны включить перетаскивание на картах, поменять фазу на строительств, поменять кнопку, на закончить фазу строительства, так же данная фаза должна включать добавление маны с домиков.
      buildPhase = "Build phase";
      beginBuildPhase = true;
      _phaseText.text = buildPhase;
      _buttonsEndPhase.text = "End build phase";
   }

   public void BeginPenutationPhase()
   {
      // тут должно быть выключено перетаскивание с руки, но при этом включино перетаскивание с одной панели на поле в другую
      permutationPhase = "Permutation phase";
      beginBuildPhase = false;
      beginPenutationFase = true;
      _phaseText.text = permutationPhase;
      _buttonsEndPhase.text = "End permutation phase";
   }

   public void BeginBattle()
   {
      #region ChangeFase
      battlePhase = "Battle phase";
      beginPenutationFase = false;
      _beginBattleFase = true;
      _phaseText.text = battlePhase;
      _buttonsEndPhase.text = "End battle phase";
      #endregion
      _checkEnemy.MonstersAttack();
   }

   public void EndPhase()
   {
      // момент сброса карт с руки и передача хода противнику
      endPhase = "End phase";
      _beginBattleFase = false;
      _beginEndPhase = true;
      _phaseText.text = endPhase;
      _buttonsEndPhase.text = "End turn";
   }
}
