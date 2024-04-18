using System.Collections.Generic;
using UnityEngine;


public static class AssigningAttackers
{
     private static Dictionary <GameObject, GameObject> attackersAndTargetDictionary = new Dictionary<GameObject, GameObject>();
     
     public static void AddAttackerAndTarget(GameObject attacker, GameObject target)
     {
          if (attackersAndTargetDictionary.ContainsKey(attacker))
          {
               attackersAndTargetDictionary[attacker] = target;
          }
          else
          {
               attackersAndTargetDictionary.Add(attacker, target);
          }
          
     }

     public static void Attack(TokenEffectOnOpponent tokenEffectOnOpponent)
     {
          foreach (var vCard in attackersAndTargetDictionary)
          {
               var AttackCreature = vCard.Key;
               var TargetCreature = vCard.Value;

               CardPrefab cardPrefab = AttackCreature.GetComponent<CardPrefab>();
               EnemyBattlePhase enemyBattlePhase = TargetCreature.GetComponent<EnemyBattlePhase>();
               AssignAttackHandler assignAttackHandler = cardPrefab.GetComponent<AssignAttackHandler>();
               
               if (cardPrefab != null)
               {
                    cardPrefab.DealDamage(TargetCreature);
                    assignAttackHandler.OffAssigningAttackers();
                    
                    tokenEffectOnOpponent.AddNewTokenEnemy(enemyBattlePhase, cardPrefab);
                    Debug.Log("Add token");
               }
               else
               {
                    Debug.LogError("Error: Attacker or Target card is missing CardPrefab component.");
               }
          }
          attackersAndTargetDictionary.Clear();
     }
     
     public static void RemoveAttacker(GameObject attacker, GameObject target)
     {
          attackersAndTargetDictionary.Remove(attacker);
     }
}
