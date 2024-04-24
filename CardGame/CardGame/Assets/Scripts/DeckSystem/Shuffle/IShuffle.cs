using System.Collections.Generic;

namespace Deck.Shuffle
{
    public interface IShuffle
    {
        void ShuffleDeckAndPool<T1>(List<T1> list);
    }
}