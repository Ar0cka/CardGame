using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DeckController : MonoBehaviour
{
    #region SerializeListCards

    [SerializeField] private List<CardPrefab> _cardsInDeck;
    [SerializeField] private List<CardPrefab> _trashCards;
    [SerializeField] private HandCards _handCards;
    
    #endregion

    #region UI

    [SerializeField] private TextMeshProUGUI _amountDeckCard; 
    [SerializeField] private TextMeshProUGUI _amountTrashCard;
    
    #endregion
    
    private void Awake()
    {
        _handCards.DrawNextCard(_cardsInDeck);
        _amountDeckCard.text = _cardsInDeck.Count.ToString( );
    }

    public void TakeCardInHand()
    {
        _handCards.DrawNextCard(_cardsInDeck);
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
    }
}
