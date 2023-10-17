using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class EnemyData
{
    public string name;
    public int hp;
    public int damage;
    public Sprite spriteMonster;
}

public class EnemyTransitController : MonoBehaviour
{
    [SerializeField] private List<EnemyInfo> _monsters;

    public void SaveMonsterData()
    {

        string jsonData = JsonUtility.ToJson(_monsters);
        PlayerPrefs.SetString("MonsterData", jsonData);
        PlayerPrefs.Save();
    }

    public List<EnemyInfo> LoadMonsterData()
    {
        string jsonData = PlayerPrefs.GetString("MonsterData", "");
        if (!string.IsNullOrEmpty(jsonData))
        {
            return JsonUtility.FromJson<List<EnemyInfo>>(jsonData);
        }

        return new List<EnemyInfo>();
    }
}
