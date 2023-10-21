using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginBattleFase : MonoBehaviour
{
    [SerializeField] private string nameScene;
    [SerializeField] private GameObject enemyHero;
    private EnemyTransit _monsterTransit;
    private Monsters _monsters;

    private void Start()
    {
        _monsters = GetComponent<Monsters>();
        _monsterTransit = FindObjectOfType<EnemyTransit>();
    }

    public void BeginBattle()
    {
        DontDestroyOnLoad(_monsterTransit);
        
        _monsterTransit._enemyList.Clear();
        _monsterTransit._enemyList = _monsters._monstersList;
        
        _monsters.SaveMonsterData();
        
        SceneManager.LoadScene(nameScene);
    }
}
