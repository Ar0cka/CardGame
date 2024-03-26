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
        
        AttackUnits();
        
        _playerBattleScene.hitHero(_enemySettings.damage);
        _enemyAndPlayerUI.UpgradeHPBardPlayer();
    }
    private IEnumerator StartDelayAttackEnemy()
    {
        yield return new WaitForSeconds(1);
    }
    
    public void AttackEnemy(int damageHero)
    {
        _enemySettings._currentHitPointsEnemy -= damageHero;
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }

    private void PlayAnimations()
    {
        _animator.SetTrigger("IsAttack");
        StartDelayAttackEnemy();
    }
    
    public void CheackDefensers()
    {
        AssigningDefense.DefenseEnemy(ref _defensers);
    }
    
    public void AttackUnits()
    {
        if (_defensers.Count > 0)
        {
            foreach (var vCard in _defensers)
            {
                vCard.TakeDamage(ref _summaEnemyAttack);
                PlayAnimations();
            
                AttackEnemy(vCard._cardInfo.damage);
            }
        }

        if (_summaEnemyAttack > 0)
        {
            AttackPlayer();
        }
    }
    
}
