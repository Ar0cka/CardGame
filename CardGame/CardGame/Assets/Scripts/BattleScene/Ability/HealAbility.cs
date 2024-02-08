using System;
using UnityEngine;


public class HealAbility : MonoBehaviour, IAbility
{
    private PlayerBattleScene _player;
    private ManagerAbility _managerAbility;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerBattleScene>();
        _managerAbility = FindObjectOfType<ManagerAbility>();
    }

    private void OnEnable()
    {
        _managerAbility.OnCardDroped += ActivatedAbility;
    }

    private void OnDisable()
    {
        _managerAbility.OnCardDroped -= ActivatedAbility;
    }

    public void ActivatedAbility(CardInfo cardInfo)
    {
        _player.currentHp += cardInfo.heal;
        OnDisable();
    }   
}
