using System;
using UnityEngine;

public class CheckEnemyType : MonoBehaviour
{
    private bool isMonster, isPlayer;
    private PlayerBattleScene _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerBattleScene>();
    }

    public void MonstersAttack()
    {
        _player.AttackMonsters();
    }
}
