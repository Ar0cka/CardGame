using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ManaManager : MonoBehaviour // Менеджер по добавлению маны в пул
{
   private List<ManaCardsPrefab> buildResurses = new List<ManaCardsPrefab>();
   private List<ManaCardsPrefab> humanResurses = new List<ManaCardsPrefab>();
   private PlayerBattleScene player;

   [SerializeField] private EnemyAndPlayerUI _playerUI;
   
   public void InitializeManaManager()
   {
      player = FindObjectOfType<PlayerBattleScene>();
   }

   public void RegisterCards(ManaCardsPrefab prefabCard, ResursBuild resursBuildInfo)
   {
      if (resursBuildInfo.typeBuild == ResursBuild.TypeBuild.BuildMana)
      {
         buildResurses.Add(prefabCard);
      }
      else if (resursBuildInfo.typeBuild == ResursBuild.TypeBuild.HumanMana)
      {
         humanResurses.Add(prefabCard);
      }
   }

   public void AddManaToPool()
   {
      player.UpgradeMana(buildResurses.Count, humanResurses.Count);
      _playerUI.UpgradeManaPool(player.manaBuild, player.manaHuman);
   }
}
 