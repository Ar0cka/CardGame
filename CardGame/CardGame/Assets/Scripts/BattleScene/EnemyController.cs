using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    #region EnemyRegion
    
    private List<EnemyInfo> enemyInfo;
    private SpriteRenderer _spriteRenderer;
    private EnemyTransit _monstersTransit;
    
    #endregion

    #region Components

    private PlayerBattleScene _playerBattleScene;
    private EnemyAndPlayerUI _enemyAndPlayerUI;

    #endregion

    #region MonstersParametrs
    
    private string _name;
    private int _damage, _maxHitPoints,
        _defense;

    private Sprite _monsterSprite;
    #endregion

    public int _currentHitPoints;

    public void InitializeEnemyController()
    {
        #region SerilizeComponent

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _monstersTransit = FindObjectOfType<EnemyTransit>();
        _playerBattleScene = FindObjectOfType<PlayerBattleScene>();
        _enemyAndPlayerUI = FindObjectOfType<EnemyAndPlayerUI>();

        #endregion

        #region LoadMonster

        enemyInfo = _monstersTransit.LoadMonsterData();
        for (int i = 0; i < enemyInfo.Count; i++)
        {
            _name = enemyInfo[i].nameEnemy;
            _maxHitPoints = enemyInfo[i].hp;
            _damage = enemyInfo[i].damage;
            _defense = enemyInfo[i].defense;
            _monsterSprite = enemyInfo[i].monsterSprite;
        }
        _currentHitPoints = _maxHitPoints;
        LoadSpriteMonster();

        #endregion

        _enemyAndPlayerUI.UpgradeUIEnemy();
    }
    
    private void LoadSpriteMonster()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = _monsterSprite;
    }

    public void AttackPlayer()
    {
        _playerBattleScene.hitHero(_damage);
        _enemyAndPlayerUI.UpgradeUiPlayer();
    }

    public int AttackEnemy(int damageHero)
    {
        _currentHitPoints -= damageHero;
        return _currentHitPoints;
    }

    public void DefenseEnemy()
    {
        _currentHitPoints += _defense;
    }
}
