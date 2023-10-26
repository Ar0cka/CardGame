using System;
using UnityEngine;

public class PlayerBattleScene : MonoBehaviour, IPlayerBattleScene
{
    [SerializeField] private int _mana;
    [SerializeField] private int _health;
    [HideInInspector] public int currentHp;

    public int Health { get; set; }
    public int Mana => _mana;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentHp = _health;
    }

    public void UpgradeMana()
    {
        _mana++;
    }

    public void hitHero(int damage)
    {
        currentHp -= damage;
    }
}
