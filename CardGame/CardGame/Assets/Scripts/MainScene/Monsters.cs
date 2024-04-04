using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class EnemyData
{
    public string name;
    public int hp, damage, defense, buffDamage;
    public Sprite spriteMonster;
}

[RequireComponent(typeof(SpriteRenderer))]
public class Monsters : MonoBehaviour
{
    [SerializeField] private List<EnemyInfo> monsterList;
    public List<EnemyInfo> _monstersList => monsterList;


    public void SaveMonsterData()
    {
        for (int i = 0; i < _monstersList.Count; i++)
        {
            EnemyInfo enemy = _monstersList[i];
            EnemyData enemyData = new EnemyData();
            enemyData.name = enemy.nameEnemy;
            enemyData.hp = enemy.hp;
            enemyData.damage = enemy.damage;
            enemyData.defense = enemy.defense;
            enemyData.spriteMonster = enemy.monsterSprite;
            enemyData.buffDamage = enemy.buff;

            string jsonData = JsonUtility.ToJson(enemyData);
            PlayerPrefs.SetString("MonsterData_" + i, jsonData);
        }
        PlayerPrefs.Save();
    }
}
