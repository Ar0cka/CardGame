using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbility : MonoBehaviour, IAbility
{
    private EnemyController _enemy;
    private EnemyAndPlayerUI _enemyAndPlayerUI;

    private void Awake()
    {
        _enemy = FindObjectOfType<EnemyController>();
        _enemyAndPlayerUI = FindObjectOfType<EnemyAndPlayerUI>();
    }

    public void ActivatedAbility(CardInfo cardInfo)
    {
        _enemy.AttackEnemy(cardInfo.damage);
        _enemyAndPlayerUI.UpgradeHPBarEnemy();
    }
}
