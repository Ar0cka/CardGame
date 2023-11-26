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
    [SerializeField] private SpriteRenderer _monsterSprite, _playerRenderer;
    [SerializeField] private BoxCollider2D _colliderEnemy, _coliderPlayer;
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

        #region OffColiderAndSprite

        _movePlayer.enabled = false;
        _playerRenderer.enabled = false;
        _monsterSprite.enabled = false;
        _coliderPlayer.enabled = false;
        _colliderEnemy.enabled = false;

        #endregion
        
        
        SceneManager.LoadScene(nameScene);
    }
}
