using System;
using UnityEngine;


public class HealAbility : MonoBehaviour, IAbility
{
    private PlayerBattleScene _player;
    private EnemyAndPlayerUI _playerUI;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerBattleScene>();
        _playerUI = FindObjectOfType<EnemyAndPlayerUI>();
    }

    public void ActivatedAbility(CardInfo cardInfo)
    {
        _player.HealHero(cardInfo.heal);
        _playerUI.UpgradeHPBardPlayer();
    }   
}
