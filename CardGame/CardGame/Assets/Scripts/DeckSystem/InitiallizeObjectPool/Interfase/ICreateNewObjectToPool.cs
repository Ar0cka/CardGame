using UnityEngine;

namespace Deck.InitiallizeObjectPool.Interfase
{
    public interface ICreateNewObjectToPool
    {
        void CreateNewObjectToPool(CardPrefab cardPref, RectTransform _handTransform);
        void CreateObjectToBattelZonePool(CardPrefab _cardPrefab);
    }
}