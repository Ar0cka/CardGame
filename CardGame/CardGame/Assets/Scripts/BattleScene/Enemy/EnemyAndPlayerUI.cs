using System;
using TMPro;
using UnityEngine;

public class EnemyAndPlayerUI : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI hpBarPlayer;
     [SerializeField] private TextMeshProUGUI hpBarEnemy;
     [SerializeField] private TextMeshProUGUI manaPool;

     private PlayerBattleScene _playerBattleScene;
     [SerializeField] private EnemyController _enemyController;

     public void InitializeUI()
     {
          _playerBattleScene = FindObjectOfType<PlayerBattleScene>();
          _enemyController = FindObjectOfType<EnemyController>();
          #region ChangeHpBars

          UpgradeHPBardPlayer();

          #endregion
     }

     public void UpgradeHPBardPlayer()
     {
          hpBarPlayer.text = _playerBattleScene.currentHp.ToString();
     }

     public void UpgradeHPBarEnemy()
     {
          hpBarEnemy.text = _enemyController._currentHitPoints.ToString();
     }

     public void UpgradeManaPool(int buildMana, int humanMana)
     {
          manaPool.text = $"Build mana: {buildMana}\n Human mana {humanMana}";
     }
     
}
