using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyTransit : MonoBehaviour
{
    public List<EnemyInfo> _enemyList;
    public List<EnemyInfo> LoadMonsterData()
    {
        List<EnemyInfo> loadedMonsters = new List<EnemyInfo>();
        for (int i = 0; i < _enemyList.Count; i++)
        {
            string jsonData = PlayerPrefs.GetString("MonsterData_" + i, "");
            if (!string.IsNullOrEmpty(jsonData))
            {
                EnemyData enemyData = JsonUtility.FromJson<EnemyData>(jsonData);
                EnemyInfo loadedMonster =  ScriptableObject.CreateInstance<EnemyInfo>();
                loadedMonster.LoadSetting(enemyData.name, enemyData.damage, enemyData.hp, enemyData.defense, enemyData.buffDamage, enemyData.spriteMonster);
                loadedMonsters.Add(loadedMonster);
            }
        }
        return loadedMonsters;
    }
}
