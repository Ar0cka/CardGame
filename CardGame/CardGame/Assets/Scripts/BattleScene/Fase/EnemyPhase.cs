using System;
using UnityEngine;


public class EnemyPhase : MonoBehaviour
{ 
    [SerializeField] private string preparationEnemy, assignDefense, attackPhase;
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
         // Выбор между бафом и атакой
     }

     public void IsAssingDefense()
     {
         // это фаза игрока на которой он сможет спокойно назначить атакующих
     }

     public void AttackEnemyPhase()
     {
         // Фаза в которая проходит автоматически. В этот момент противник атакуют по назначенным кричам или бьет в плеера (или то и другое) 
     }
}