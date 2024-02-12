using System;
using UnityEngine;

public class PlayerBattleScene : MonoBehaviour
{
    [SerializeField] private AttackMonster _attack;
    
    private int _manaBuild;
    private int _manaHuman;
    [SerializeField] private int _health;
    /*[HideInInspector]*/ public int currentHp;
    
    public int manaBuild => _manaBuild;
    public int manaHuman => _manaHuman;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentHp = _health;
    }

    public void UpgradeMana(int manaHumanAddCount, int manaBuildAddCount)
    {
        _manaHuman += manaHumanAddCount;
        _manaBuild += manaBuildAddCount;
    }

    public void hitHero(int damage)
    {
        currentHp -= damage;
    }

    public void HealHero(int heal)
    {
        currentHp += heal;
    }

    public void TakingAwayBuildManaFromManaPool(int manaCost)
    {
        _manaBuild -= manaCost;
    }
    public void TakingAwayHumandManaFromManaPool(int manaCost)
    {
        _manaHuman -= manaCost;
    }

    public void AttackMonsters()
    {
        _attack.GetCardPrefabFromPanel();
        _attack.DamageMonsters();
    }
}
