using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyBattlePhase : MonoBehaviour
{
    private EnemySettings _enemySettings;
    
    private PlayerBattleScene _playerBattleScene;
    private EnemyAndPlayerUI _enemyAndPlayerUI;
    
    private Animator _animator;

    private int _summaEnemyAttack;
    
    private int _defense;
    public int defense => _defense;

    private List<CardPrefab> _defensers = new List<CardPrefab>();
    private void Awake()
    {
        _playerBattleScene = FindObjectOfType<PlayerBattleScene>();
        _enemyAndPlayerUI = FindObjectOfType<EnemyAndPlayerUI>();
        _enemySettings = GetComponent<EnemySettings>();
        _animator = GetComponent<Animator>();
        
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }


    #region Attack

    public void AttackPlayer()
    {
        AttackUnits();
        
        _enemyAndPlayerUI.UpgradeHPBardPlayer();
    }
    private IEnumerator StartDelayAttackEnemy()
    {
        yield return new WaitForSeconds(2.5f);
    }

    private void PlayAnimations()
    {
        _animator.SetTrigger("IsAttack");
        StartCoroutine(StartDelayAttackEnemy());
    }
    
    public void AttackUnits()
    {
        CheackDefensers(ref _defensers);
        _summaEnemyAttack = _enemySettings.damage;
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
            _playerBattleScene.hitHero(_summaEnemyAttack);
        }
        AssigningDefense.ClearDictionaryDefensers();
    }
    
    #endregion
    public void AttackEnemy(int damageHero)
    {
        if (_defense > 0)
        {
            _defense -= damageHero;
            damageHero -= _defense;
        }

        if (damageHero > 0)
        {
            _enemySettings._currentHitPointsEnemy -= damageHero;
        }
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }

    public void Defense()
    {
        _defense = Random.Range(8, 10);
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }

    public void BeginTurnEnemy()
    {
        if (_defense > 0)
        _defense = 0;
        
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }
    
    public void Buff()
    {
        _enemySettings.Buff();
    }
    
    public void CheackDefensers(ref List<CardPrefab> defensers)
    {
        AssigningDefense.DefenseEnemy(ref defensers);
    }
}
