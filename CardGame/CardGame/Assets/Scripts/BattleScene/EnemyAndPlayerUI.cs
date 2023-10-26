using System;
using TMPro;
using UnityEngine;

public class EnemyAndPlayerUI : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI hpBarPlayer;
     [SerializeField] private TextMeshProUGUI hpBarEnemy;

     private PlayerBattleScene _playerBattleScene;
     private EnemyController _enemyController;

     private void Awake()
     {
          _playerBattleScene = FindObjectOfType<PlayerBattleScene>();
          _enemyController = FindObjectOfType<EnemyController>();

          #region ChangeHpBars

          UpgradeUiPlayer();

          #endregion
     }

     public void UpgradeUiPlayer()
     {
          hpBarPlayer.text = _playerBattleScene.currentHp.ToString();
     }

     public void UpgradeUIEnemy()
     {
          hpBarEnemy.text = _enemyController._currentHitPoints.ToString();
     }
}
