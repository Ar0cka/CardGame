using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbility : MonoBehaviour, IAbility
{
    private EnemySettings _enemy;
    private EnemyAndPlayerUI _enemyAndPlayerUI;

    private void Awake()
    {
        _enemy = FindObjectOfType<EnemySettings>();
        _enemyAndPlayerUI = FindObjectOfType<EnemyAndPlayerUI>();
    }

    public void ActivatedAbility(CardInfo cardInfo)
    {
        _enemy.AttackEnemy(cardInfo.damage);
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }
}
