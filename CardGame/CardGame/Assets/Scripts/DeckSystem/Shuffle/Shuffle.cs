using System;
using System.Collections.Generic;

namespace Deck.Shuffle
{
    public class Shuffle : IShuffle
    {
        private static Random rng = new Random();
        
        public void ShuffleDeckAndPool<T1>(List<T1> list)
        {
            int _deckCount = list.Count;
            for (int i = _deckCount - 1; i > 0; i--)
            {
                int j = rng.Next(0, i + 1);
                T1 tempCard = list[i];
                list[i] = list[j];
                
                list[j] = tempCard;
            }
        }

    }
}