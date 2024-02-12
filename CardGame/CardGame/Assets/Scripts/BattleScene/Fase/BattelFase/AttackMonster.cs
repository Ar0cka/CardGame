using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonoBehaviour
{
   private DropCardInPanel panelsCardsPrefabs;
   private EnemyController _enemyController;
   private EnemyAndPlayerUI _enemyAndPlayerUI;
   
   private List<CardPrefab> _cardsInMilyArmyZone = new List<CardPrefab>();
   private List<CardPrefab> _cardsInRangeHumanZone = new List<CardPrefab>();
   private List<CardPrefab> _cardsInRangeBuildZone = new List<CardPrefab>();
   
   private int damageAllCards;

   public void GetCardPrefabFromPanel()
   {
      if (panelsCardsPrefabs == null)
      panelsCardsPrefabs = FindObjectOfType<DropCardInPanel>();
      
      List<CardPrefab> _cards = new List<CardPrefab>();
      
      panelsCardsPrefabs.repackAllLists(_cards);

      for (int i = 0; i < _cards.Count; i++)
      {
         if (_cards[i]._cardInfo.subtype == CardInfo.SubtypeCard.AttackHuman ||
             _cards[i]._cardInfo.subtype == CardInfo.SubtypeCard.DefenseBuild)
         {
            _cardsInMilyArmyZone.Add(_cards[i]);
         }
         else if (_cards[i]._cardInfo.subtype == CardInfo.SubtypeCard.AttackRangeHuman)
         {
            _cardsInRangeHumanZone.Add(_cards[i]);
         }
         else if (_cards[i]._cardInfo.subtype == CardInfo.SubtypeCard.AuxiliaryBuild ||
                  _cards[i]._cardInfo.subtype == CardInfo.SubtypeCard.AttackRangeBuild)
         {
            _cardsInRangeBuildZone.Add(_cards[i]);
         }
      }
   }

   public void DamageMonsters()
   {
      if (_enemyAndPlayerUI == null)
         _enemyAndPlayerUI = FindObjectOfType<EnemyAndPlayerUI>();
      if (_enemyController == null)
         _enemyController = FindObjectOfType<EnemyController>();
      
      foreach (var cards in _cardsInMilyArmyZone)
      {
         damageAllCards += cards._cardInfo.damage;
      }

      foreach (var cards in _cardsInRangeBuildZone)
      {
         damageAllCards += cards._cardInfo.damage;
      }
      
      foreach (var cards in _cardsInRangeHumanZone)
      {
         damageAllCards += cards._cardInfo.damage;
      }
      
      _enemyController.AttackEnemy(damageAllCards);
      _enemyAndPlayerUI.UpgradeHPBarEnemy();
      
      _cardsInMilyArmyZone.Clear();
      _cardsInRangeHumanZone.Clear();
      _cardsInRangeBuildZone.Clear();
   }
   
}
