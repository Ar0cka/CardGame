using System.Collections.Generic;
using CardSettings.Tokens;
using Turn.PhaseSettings.BattelPhase;
using UnityEngine;
using Zenject;


public class AssigningAttackers : IAttack, IAddAttakerAndTarget
{
     [Inject] private ITokenEffectOnOpponent tokenEffectOnOpponent;
     
     private Dictionary <GameObject, GameObject> attackersAndTargetDictionary = new Dictionary<GameObject, GameObject>();
     
     public void AddAttackerAndTarget(GameObject attacker, GameObject target)
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

     public void Attack()
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
     
     public void RemoveAttacker(GameObject attacker, GameObject target)
     {
          attackersAndTargetDictionary.Remove(attacker);
     }
}
