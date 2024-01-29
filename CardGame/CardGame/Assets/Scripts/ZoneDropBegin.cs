using System;
using System.Collections.Generic;
using UnityEngine;


public class ZoneDropBegin : MonoBehaviour
{
   [SerializeField] private List<ManaCardsPrefab> _zoneCards;
   [SerializeField] private Transform _transform;
   

   [SerializeField] private ManaManager _manaManager;

    public void InitializeZoneDrop()
   {
      for (int i = 0; i < _zoneCards.Count; i++)
      {
         var cardPref = Instantiate(_zoneCards[i], _transform);
         _manaManager.RegisterCards(cardPref, _zoneCards[i]._cardInfo);
      }
   }
}
