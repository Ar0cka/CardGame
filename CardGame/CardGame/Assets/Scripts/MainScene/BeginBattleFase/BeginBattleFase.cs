using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginBattleFase : MonoBehaviour
{
    [SerializeField] private string nameScene;
    [SerializeField] private GameObject enemyHero;
    [SerializeField] private PlayerMove _movePlayer;
    [SerializeField] private SpriteRenderer _monsterSprite;
    [SerializeField] private SpriteRenderer _playerRenderer;
    private EnemyTransit _monsterTransit;
    private Monsters _monsters;

    private void Start()
    {
        _monsters = GetComponent<Monsters>();
        _monsterTransit = FindObjectOfType<EnemyTransit>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BeginBattle();
        }
    }
    
    public void BeginBattle()
    {
        DontDestroyOnLoad(_monsterTransit);
        
        _monsterTransit._enemyList.Clear();
        _monsterTransit._enemyList = _monsters._monstersList;
        
        _monsters.SaveMonsterData();

        _movePlayer.enabled = false;
        _playerRenderer.enabled = false;
        _monsterSprite.enabled = false;
        
        SceneManager.LoadScene(nameScene);
    }
}
