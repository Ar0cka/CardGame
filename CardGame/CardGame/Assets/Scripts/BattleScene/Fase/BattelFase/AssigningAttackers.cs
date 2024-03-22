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

     public static void Attack()
     {
          foreach (var vCard in attackersAndTargetDictionary)
          {
               var AttackCreature = vCard.Key;
               var TargetCreature = vCard.Value;

               CardPrefab cardPrefab = AttackCreature.GetComponent<CardPrefab>();
               AttackHandler attackHandler = cardPrefab.GetComponent<AttackHandler>();
               
               if (cardPrefab != null)
               {
                    cardPrefab.DealDamage(TargetCreature);
                    attackHandler.OffAssigningAttackers();
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
