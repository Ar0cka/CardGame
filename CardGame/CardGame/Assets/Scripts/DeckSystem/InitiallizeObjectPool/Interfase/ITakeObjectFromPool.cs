using Deck.Shuffle;
using UnityEngine;

namespace Deck.InitiallizeObjectPool.Interfase
{
    public interface ITakeObjectFromPool
    {
        GameObject GetObjectFromPool(GameObject card);
        void GetMaxSizePool(int deckCount);
        void ShufflePool(IShuffle _shuffle);
    }
}