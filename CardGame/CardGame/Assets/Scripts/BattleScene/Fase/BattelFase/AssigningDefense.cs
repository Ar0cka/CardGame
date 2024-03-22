
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class AssigningDefense
{
    private static Dictionary<GameObject, GameObject> _assigningDefense = new Dictionary<GameObject, GameObject>();

    public static void AddNewDefense(GameObject defenser, GameObject attacker)
    {
        _assigningDefense.Add(defenser, attacker);
    }

    public static void DefenseIfEnemyHaveCuttingDamage(int SummaAttacks)
    {
        foreach (var vCard in _assigningDefense)
        {
            var defense = vCard.Value;
            var target = vCard.Value;

            CardPrefab cardPrefab = defense.GetComponent<CardPrefab>();
            DropCardInPanel _drop = defense.GetComponent<DropCardInPanel>();
            
            if (SummaAttacks != 0)
            {
                cardPrefab.ReturnHP(ref SummaAttacks);
                cardPrefab.DealDamage(target);
                
                Debug.Log("Summa Attack " + SummaAttacks);

                if (cardPrefab._currentHitPoint <= 0)
                {
                     RemoveDefenser(defense);
                     Debug.Log($"Card: {cardPrefab._cardInfo.nameCard} dead");
                } 
                RemoveDefenser(defense);
            }
        }

        if (SummaAttacks > 0)
        {
            // урон в лицо
        }
    }

    public static void DefenseIfEnemyAttack()
    {
        
    }

    public static void RemoveDefenser(GameObject defenser)
    {
        _assigningDefense.Remove(defenser);
    }
}