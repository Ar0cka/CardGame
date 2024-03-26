using System;
using UnityEngine;


public class EnemyPhase : MonoBehaviour
{ 
   
    
     private EnemyBattlePhase _enemyBattlePhase;
    
     private bool isPreparationPhase = false; 
     private bool isAssignDefense = false;
     private bool isAttackPhase = false;

     private void Awake()
     {
         _enemyBattlePhase = FindObjectOfType<EnemyBattlePhase>();
     }

     public void EnemyPreparationPhase()
     {
         // Выбор между бафом и атакой автоматическая фаза
     }

     public void IsAssingDefense()
     {
         // это фаза игрока на которой он сможет спокойно назначить атакующих
     }

     public void AttackEnemyPhase(ref bool turnEnemy, ref bool turnPlayer)
     {
         _enemyBattlePhase.AttackPlayer();

         turnEnemy = false;
         turnPlayer = true;
     }
}