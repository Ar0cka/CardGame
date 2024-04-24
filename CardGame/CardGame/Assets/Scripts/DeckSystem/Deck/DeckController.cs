using System.Collections.Generic;
using Deck.InitiallizeObjectPool.Interfase;
using Deck.Shuffle;
using UnityEngine;
using Zenject;

#region summary
// 1. Реализовать Deck
// 2. Реализовать DiscardDeck
// 3. Подключить UI элементы к Deck
// 4. Сделать Shuffle колоды
// 5. Сделать возврат колоды, когда Deck.Count = 0
// 6. Сделать метод, который будет инициализроваться при заходе на сцену, в него будет входить Добор и создание объектов на сцене
#endregion

public class DeckController : MonoBehaviour
{
   [SerializeField] private RectTransform _handTransform;
   [SerializeField] private HandCards _handCards;
   [SerializeField] private DeckUI _deckUI;
   
   [Inject] private IShuffle _shuffle;
   [Inject] private ITakeObjectFromPool _takeObjectFromPool;
   [Inject] private ICreateNewObjectToPool _createNewObjectToPool;
   
   #region InitializeListDeck

   [SerializeField] private List<CardPrefab> _deckList;
   [SerializeField] private List<CardPrefab> _discardDeckList;
   
   #endregion
    
   public void Initialize() // метод в которой будет происходить первичная инициализация
   {
      _takeObjectFromPool.GetMaxSizePool(_deckList.Count);
      CreateObject();
      
      _deckUI.UpdateUIDeck(_deckList);
      _deckUI.UpdateUIDiscardDeck(_discardDeckList);
   }
   
   private void CreateObject()
   {
      for (int i = 0; i < _deckList.Count; i++)
      {
         _deckList[i].SetUniqCode();
         _createNewObjectToPool.CreateNewObjectToPool(_deckList[i], _handTransform);
      }
      _shuffle.ShuffleDeckAndPool(_deckList);
      _takeObjectFromPool.ShufflePool(_shuffle);
   }

   public void TakeCardFromDeck()
   {
      _handCards.DrawCard(_deckList, _discardDeckList);
   }
   
   public void DiscardCardFromBattleZone(CardPrefab cardPrefab)
   {
      _discardDeckList.Add(cardPrefab);
      _deckUI.UpdateUIDiscardDeck(_discardDeckList);
   }

   public void DiscardCardFromHand()
   {
      _handCards.DiscardCard(_discardDeckList);
   }

   public void ReturnDeck()
   {
      for (int i = _discardDeckList.Count - 1; i >= 0; i--)
      {
         CardPrefab card = _discardDeckList[i];
         _deckList.Add(card);
         _discardDeckList.RemoveAt(i);
         _deckUI.UpdateUIDiscardDeck(_discardDeckList);
         _deckUI.UpdateUIDeck(_deckList);
      }
      _shuffle.ShuffleDeckAndPool(_deckList);
      _takeObjectFromPool.ShufflePool(_shuffle);
   }
}
