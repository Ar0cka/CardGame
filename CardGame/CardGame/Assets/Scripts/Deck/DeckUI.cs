using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DeckUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountDeck, amountDiscardDeck;
    
    public void UpdateUIDeck(List<CardPrefab> _deckList)
    {
        amountDeck.text = _deckList.Count.ToString();
    }
   
    public void UpdateUIDiscardDeck(List<CardPrefab> _discardList)
    {
        amountDiscardDeck.text = _discardList.Count.ToString();
    }
    
}
