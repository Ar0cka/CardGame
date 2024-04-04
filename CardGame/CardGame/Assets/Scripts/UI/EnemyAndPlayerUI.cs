using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAndPlayerUI : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI hpBarPlayer;
     [SerializeField] private TextMeshProUGUI hpBarEnemy;
     [SerializeField] private TextMeshProUGUI defenseEnemy;
     [SerializeField] private TextMeshProUGUI manaPool;

     private PlayerBattleScene _playerBattleScene;
     private EnemySettings enemySettings;
     private EnemyBattlePhase _enemyBattlePhase;

     public void InitializeUI()
     {
          _playerBattleScene = FindObjectOfType<PlayerBattleScene>();
          enemySettings = FindObjectOfType<EnemySettings>();
          _enemyBattlePhase = FindObjectOfType<EnemyBattlePhase>();
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
          hpBarEnemy.text = $"{enemySettings.name} hit points: {enemySettings._currentHitPointsEnemy.ToString()}";
          defenseEnemy.text = $"Defense: {_enemyBattlePhase.defense.ToString()}";
     }

     public void UpgradeManaPool(int buildMana, int humanMana)
     {
          manaPool.text = $"Build mana: {buildMana}\n Human mana {humanMana}";
     }
}
