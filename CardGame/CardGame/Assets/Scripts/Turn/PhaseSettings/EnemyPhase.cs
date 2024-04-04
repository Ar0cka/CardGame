using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.UIElements;


public class EnemyPhase : MonoBehaviour
{ 
     private EnemyBattlePhase _enemyBattlePhase;
     private PlayerPhase _playerPhase;
     private TurnController _turnController;
     private DropCardInPanel _cardZoneController;
     private EnemyStateSettings enemyStateSettings = new EnemyStateSettings();

     #region boolPhase

     private bool isPreparationPhase = false; 
     private bool isAssignDefense = false;
     private bool isAttackPhase = false;
     
     public bool _isPreparationPhase => isPreparationPhase;
     public bool _isAssignDefense => isAssignDefense;
     public bool _isAttackPhase => isAttackPhase;

     #endregion

     #region boolAction

     private bool isAttack;
     private bool isDefense;
     private bool isBuff;
     
     private bool isDefenseAction;
     private bool isAttackAction;
     private bool isBuffAction;

     #endregion
     
     private List<CardPrefab> _defensers = new List<CardPrefab>();
     
     private void Awake()
     {
         _enemyBattlePhase = FindObjectOfType<EnemyBattlePhase>();
         _playerPhase = GetComponent<PlayerPhase>();
         _turnController =FindObjectOfType<TurnController>();
         _cardZoneController = FindObjectOfType<DropCardInPanel>();
         
         UpdateBoolAction();
     }
     
     #region EnemyPreparationPhase

     public void EnemyPreparationPhase()
     {
         _playerPhase.ChangePhase(ref isAttackPhase, ref isPreparationPhase, "Wait turnPlayer", "Preparation enemy phase" );
         AssigningDefense.ClearDictionaryDefensers();
         enemyStateSettings.SelectEnemyAction(ref isAttack, ref isBuff, ref isDefense);
         UpdateBoolAction();
         StartCoroutine(DelayChangePhase());
         ActionSelection();
     }
     
     private void ActionSelection()
     {
         if (isAttackAction)
         {
             AssingDefense();
             Debug.Log("Attack action");
         }
         else if (isBuffAction)
         {
             _enemyBattlePhase.Buff();
             _turnController.BeginTurnPlayer();
             Debug.Log("Buff action");
         }
         else if (isDefenseAction)
         {
             _enemyBattlePhase.Defense();
             _turnController.BeginTurnPlayer();
             Debug.Log("Defense action");
         }
     }
     
     private IEnumerator DelayChangePhase()
     {
         yield return new WaitForSeconds(3f);
     }
     
     private void UpdateBoolAction()
     {
         isAttackAction = isAttack && !isDefense && !isBuff;
         isBuffAction = isBuff && !isDefense && !isAttack;
         isDefenseAction = isDefense && !isBuff && !isAttack; 
     }
     #endregion

     #region AssingDefensePhase

     public void AssingDefense()
     {
         #region BeginPhase

         _playerPhase.ChangePhase(ref isPreparationPhase, ref isAssignDefense, "End assign defensers", "Assigns defensers");
         _turnController.SwapTurn();

         #endregion
         
         _cardZoneController.CheakBattleZone(ref _defensers);
         // взять карты которые находятся в полях BattleZone, Range zone
         
         foreach (var vCard in _defensers)
         {
             var handlerController = vCard.GetComponent<HandlerController>();
             handlerController.DefenseHandlerConroller(_isAssignDefense);
         }
     }

     #endregion

     #region AttackPhase

     public void AttackEnemyPhase()
     {
         _turnController.SwapTurn();
         _playerPhase.ChangePhase(ref isAssignDefense, ref isAttackPhase, "wait turnPlayer", "Attack enemy");
         
         foreach (var vCard in _defensers)
         {
             var handlerController = vCard.GetComponent<HandlerController>();
             handlerController.DefenseHandlerConroller(_isAssignDefense);
         } 
         _enemyBattlePhase.AttackPlayer();
         _turnController.BeginTurnPlayer();
     }

     #endregion
     

    
    
}