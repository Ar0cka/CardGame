using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAndPlayerUI : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI hpBarPlayer;
     [SerializeField] private TextMeshProUGUI hpBarEnemy;
     [SerializeField] private TextMeshProUGUI manaPool;

     private PlayerBattleScene _playerBattleScene;
     private EnemySettings enemySettings;

     public void InitializeUI()
     {
          _playerBattleScene = FindObjectOfType<PlayerBattleScene>();
          enemySettings = FindObjectOfType<EnemySettings>();
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
          hpBarEnemy.text = enemySettings._currentHitPointsEnemy.ToString();
     }

     public void UpgradeManaPool(int buildMana, int humanMana)
     {
          manaPool.text = $"Build mana: {buildMana}\n Human mana {humanMana}";
     }
     
}
