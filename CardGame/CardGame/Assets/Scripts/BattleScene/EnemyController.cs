using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour, IEnemyController
{
    [SerializeField] private List<EnemyInfo> enemyInfo;
    private EnemyTransitController _enemyTransitController;

    #region MonstersParametrs
    
    private string _name;
    private int _damage, _maxHitPoints, _currentHitPoints, 
        _defense;

    private Sprite _monsterSprite;
    #endregion

    
    private void Awake()
    {
        _enemyTransitController.GetComponent<EnemyTransitController>();
        enemyInfo = _enemyTransitController.LoadMonsterData();
        for (int i = 0; i < enemyInfo.Count; i++)
        {
            _name = enemyInfo[i]._name;
            _maxHitPoints = enemyInfo[i]._hp;
            _damage = enemyInfo[i]._damage;
            _defense = enemyInfo[i]._defense;
            _monsterSprite = enemyInfo[i]._monsterSprite;
        }
        
        _currentHitPoints = _maxHitPoints;
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
