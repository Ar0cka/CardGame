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
        if (cardInfo is AuxiliaryBuild)
        {
            AuxiliaryBuild auxiliaryBuild = (AuxiliaryBuild)cardInfo;
            _player.HealHero(auxiliaryBuild.heal);
        }
        _playerUI.UpgradeHPBardPlayer();
    }   
}
