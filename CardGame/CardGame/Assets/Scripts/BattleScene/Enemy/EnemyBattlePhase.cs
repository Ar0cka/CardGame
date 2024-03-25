using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class EnemyBattlePhase : MonoBehaviour
{
    private EnemySettings _enemySettings;
    
    private PlayerBattleScene _playerBattleScene;
    private EnemyAndPlayerUI _enemyAndPlayerUI;
    
    private Animator _animator;

    private int _summaEnemyAttack;

    private List<CardPrefab> _defensers = new List<CardPrefab>();

    private void Awake()
    {
        _playerBattleScene = FindObjectOfType<PlayerBattleScene>();
        _enemyAndPlayerUI = FindObjectOfType<EnemyAndPlayerUI>();
        _enemySettings = GetComponent<EnemySettings>();
        _animator = GetComponent<Animator>();
        
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }

    public void AttackPlayer()
    {
        if (_defensers.Count != 0 && _defensers != null)
        {
            AttackUnits();
        }
        
        _animator.SetTrigger("IsAttack");
        StartDelayAttackEnemy();
        
        _playerBattleScene.hitHero(_enemySettings.damage);
        _enemyAndPlayerUI.UpgradeHPBardPlayer();
    }
    private IEnumerator StartDelayAttackEnemy()
    {
        yield return new WaitForSeconds(3);
    }
    
    public void AttackEnemy(int damageHero)
    {
        _enemySettings._currentHitPointsEnemy -= damageHero;
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }

    public void CheackDefensers()
    {
        AssigningDefense.DefenseEnemy(ref _defensers);
    }
    
    public void AttackUnits()
    {
        foreach (var vCard in _defensers)
        {
            vCard.TakeDamage(ref _summaEnemyAttack);
            
            AttackEnemy(vCard._cardInfo.damage);
        }
    }
    
}
