
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

    public static void DefenseEnemy(ref List<CardPrefab> defenser)
    {
        foreach (var vCard in _assigningDefense)
        {
            var defense = vCard.Key;
            var target = vCard.Value;

            CardPrefab cardPrefab = defense.GetComponent<CardPrefab>();
            CardZoneController _drop = defense.GetComponent<CardZoneController>();
            
            defenser.Add(cardPrefab);
        }
    }

    public static void RemoveDefenser(GameObject defenser)
    {
        _assigningDefense.Remove(defenser);
    }

    public static void ClearDictionaryDefensers()
    {
        _assigningDefense.Clear();
    }
}