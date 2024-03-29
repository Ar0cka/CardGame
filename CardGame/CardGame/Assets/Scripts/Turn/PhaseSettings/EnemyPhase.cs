using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class EnemyPhase : MonoBehaviour
{ 
     private EnemyBattlePhase _enemyBattlePhase;
     private PlayerPhase _playerPhase;
     private TurnController _turnController;
     private DropCardInPanel _cardZoneController;
    
     private bool isPreparationPhase = false; 
     private bool isAssignDefense = false;
     private bool isAttackPhase = false;

     public bool _isPreparationPhase => isPreparationPhase;
     public bool _isAssignDefense => isAssignDefense;
     public bool _isAttackPhase => isAttackPhase;

     private List<CardPrefab> _defensers = new List<CardPrefab>();
     
     private void Awake()
     {
         _enemyBattlePhase = FindObjectOfType<EnemyBattlePhase>();
         _playerPhase = GetComponent<PlayerPhase>();
         _turnController =FindObjectOfType<TurnController>();
         _cardZoneController = FindObjectOfType<DropCardInPanel>();
     }

     public void EnemyPreparationPhase()
     {
         _playerPhase.ChangePhase(ref isAttackPhase, ref isPreparationPhase, "Wait turnPlayer", "Preparation enemy phase" );
         StartCoroutine(DelayChangePhase());
        
         //Выбор атаки или бафа
         // Если атака назначить защищающих
     }

     private IEnumerator DelayChangePhase()
     {
         yield return new WaitForSeconds(3f);
         AssingDefense();
     }

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
    
     
     public void AttackEnemyPhase()
     {
         _turnController.SwapTurn();
         _playerPhase.ChangePhase(ref isAssignDefense, ref isAttackPhase, "wait turnPlayer", "Attack enemy");
         _enemyBattlePhase.AttackPlayer();
         _turnController.BeginTurnPlayer();
     }
    
}