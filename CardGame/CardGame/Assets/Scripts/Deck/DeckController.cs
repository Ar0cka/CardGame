using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

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
   private static Random rng = new Random();
   
   [SerializeField] private HandCards _handCards;
   [SerializeField] private InitializeObjectToPool _initializeObject;
   [SerializeField] private TurnController _turnController;
   
   #region InitializeListDeckAndUI

   [SerializeField] private List<CardPrefab> _deckList;
   [SerializeField] private List<CardPrefab> _discardDeckList;
   
   [SerializeField] private TextMeshProUGUI amountDeck, amountDiscardDeck;
   #endregion
    
   public void Initialize() // метод в которой будет происходить первичная инициализация
   {
      _initializeObject.GetMaxSizePool();
      CreateObject();
      
      _deckList.Clear();
      _deckList.AddRange(_initializeObject.poolHands.Select(obj => obj.GetComponent<CardPrefab>()));
      _turnController.BeginTurnPlayer();
      UpdateUIDeck(_deckList);
      UpdateUIDiscardDeck(_discardDeckList);
   }

   private void CreateObject()
   {
      for (int i = 0; i < _deckList.Count; i++)
      {
         _initializeObject.CreateNewObjectToPool(_deckList[i]);
      }
      ShuffleDeckAndPool(_deckList, _initializeObject.poolHands);
   }

   public void TakeCardFromDeck()
   {
      _handCards.DrawCard(_deckList, _discardDeckList);
   }

   public void UpdateUIDeck(List<CardPrefab> _deckList)
   {
      amountDeck.text = _deckList.Count.ToString();
   }
   
   public void UpdateUIDiscardDeck(List<CardPrefab> _discardList)
   {
      amountDiscardDeck.text = _discardList.Count.ToString();
   }

   public void DiscardCardFromBattleZone(CardPrefab cardPrefab)
   {
      _discardDeckList.Add(cardPrefab);
      UpdateUIDiscardDeck(_discardDeckList);
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
         UpdateUIDiscardDeck(_discardDeckList);
         UpdateUIDeck(_deckList);
      }
      ShuffleDeckAndPool(_deckList, _initializeObject.poolHands);
   }
   
   public void ShuffleDeckAndPool<T1, T2>(List<T1> deck, List<T2> objectPool)
   {
      int _deckCount = deck.Count;
      for (int i = _deckCount - 1; i > 0; i--)
      {
         int j = rng.Next(0, i + 1);
         T1 tempCard = deck[i];
         deck[i] = deck[j];


         T2 tempObject = objectPool[i];
         objectPool[i] = objectPool[j];

         deck[j] = tempCard;
         objectPool[j] = tempObject;
      }
   }

   public int RetunrCountDeck()
   {
      return _deckList.Count;
   }
}
