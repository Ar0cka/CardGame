using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour, IEnemyController
{
    private List<EnemyInfo> enemyInfo;
    private SpriteRenderer _spriteRenderer;
    private EnemyTransit _monstersTransit;

    #region MonstersParametrs
    
    private string _name;
    private int _damage, _maxHitPoints, _currentHitPoints, 
        _defense;

    private Sprite _monsterSprite;
    #endregion
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _monstersTransit = FindObjectOfType<EnemyTransit>();
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
    }

    private void LoadSpriteMonster()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = _monsterSprite;
    }

    public int AttackPlayer(int hpPlayer)
    {
        hpPlayer -= _damage;
        return hpPlayer;
    }

    public int AttackEnemy(int damageHero)
    {
        _currentHitPoints -= damageHero;
        return _currentHitPoints;
    }

    public int Defense(int defense)
    {
        return 0;
    }
}
