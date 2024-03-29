using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class ZoneDropBegin : MonoBehaviour
{
   [SerializeField] private List<ManaCardsPrefab> _zoneCards;
   [SerializeField] private Transform _transform;
   [FormerlySerializedAs("_manaManager")] [SerializeField] private ManaController manaController;

    public void InitializeZoneDrop()
   {
      for (int i = 0; i < _zoneCards.Count; i++)
      {
         var cardPref = Instantiate(_zoneCards[i], _transform);
         manaController.RegisterCards(cardPref, _zoneCards[i]._cardInfo);
      }
   }
}
