using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class EnemyBattlePhase : MonoBehaviour
{
    private EnemySettings _enemySettings;
    
    private PlayerBattleScene _playerBattleScene;
    private EnemyAndPlayerUI _enemyAndPlayerUI;
    
    private Animator _animator;



    private void Awake()
    {
        _playerBattleScene = FindObjectOfType<PlayerBattleScene>();
        _enemyAndPlayerUI = FindObjectOfType<EnemyAndPlayerUI>();
        _animator = GetComponent<Animator>();
        
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }

    public void AttackPlayer()
    {
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
    
}
