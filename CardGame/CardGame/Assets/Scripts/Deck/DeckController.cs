using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckController : MonoBehaviour
{
    #region SerializeListCards

    [SerializeField] private List<CardPrefab> _cardsInDeck;
    [SerializeField] private List<CardPrefab> _trashCards;
    [SerializeField] private InitializeObjectToPool _initializeObject;
    
    [SerializeField] private HandCards _handCards;
    
    #endregion

    #region UI

    [SerializeField] private TextMeshProUGUI _amountDeckCard; 
    [SerializeField] private TextMeshProUGUI _amountTrashCard;
    
    #endregion
    
    private List<int> _allIndexDeck = new List<int>();
    private List<int> _allIndexObject = new List<int>();

    public void Initialize()
    {
        ImportPoolFromDeck();
        
        TakeCardInHand();
    }
    
    public void TakeCardInHand()
    {
        _handCards.DrawNextCard(_cardsInDeck, _allIndexDeck, _allIndexObject);
        _amountDeckCard.text = _cardsInDeck.Count.ToString();
    }

    public void DiscardCards()
    {
        _handCards.Discard(_trashCards);
        _amountTrashCard.text = _trashCards.Count.ToString();
    }
    
    public void ReturnCardInDeck()
    {
        _cardsInDeck = _trashCards;
        
        _trashCards.Clear();
        
        _amountDeckCard.text = _cardsInDeck.Count.ToString();
        
        UpdateDeck();
    }

    private void ImportPoolFromDeck()
    {
        ShuffleList(_cardsInDeck);
        for (int i = 0; i < _cardsInDeck.Count; i++)
        {
            CardPrefab drawCard = _cardsInDeck[i];
            _initializeObject.CreateNewObjectToPoll(drawCard, _handCards.handTransform);
            _allIndexDeck.Add(i);
            _allIndexObject.Add(i);
        }
    }

    public void UpdateDeck()
    { 
        ShuffleList(_cardsInDeck);
        for (int i = 0; i < _cardsInDeck.Count; i++)
        {
           _allIndexDeck.Add(i);
           _allIndexObject.Add(i);
        }
        _initializeObject.ShufflePool();
    }

    public void UpdateIndex()
    {
        _allIndexDeck.Clear();
        for (int i = 0; i < _cardsInDeck.Count; i++)
        {
            _allIndexDeck.Add(i);
        }
      
    }

    public void ShuffleList<T>(List<T> DeckList)
    {
        int listCount = DeckList.Count;
        System.Random rng = new System.Random();
        while (listCount > 1)
        {
            listCount--;
            int randomIndex = rng.Next(listCount + 1);
            T value = DeckList[randomIndex];
            DeckList[randomIndex] = DeckList[listCount];
            DeckList[listCount] = value;
        }
    }
}
