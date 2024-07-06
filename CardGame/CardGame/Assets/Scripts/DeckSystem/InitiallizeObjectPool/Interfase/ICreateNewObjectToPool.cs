using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Deck.InitiallizeObjectPool.Interfase
{
    public interface ICreateNewObjectToPool
    {
        void CreateNewObjectToPool(CardPrefab cardPref, RectTransform _handTransform);
        void CreateObjectToBattelZonePool(CardPrefab _cardPrefab);
    }
}