using System.Collections.Generic;
using UnityEngine;

namespace Deck.InitiallizeObjectPool.Interfase
{
    public interface IReturnObjectToPool
    {
        void ReturnObjectInPool(GameObject objCard);
        void ReturnObjectToHandPool(CardPrefab cardPrefab);
        List<GameObject> ReturnPoolHands();
    }
}