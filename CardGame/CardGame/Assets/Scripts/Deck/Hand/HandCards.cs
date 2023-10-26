using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCards : MonoBehaviour
{
    [SerializeField] private DeckController _deckController;
    [SerializeField] private Transform handTransform;
    
    private List<CardPrefab> cardInHand = new List<CardPrefab>();
    private int maxHandSize = 5;
    private Animator animator;

    private int currentCardIndex = 0;

    public void DrawCard(List<CardPrefab> _cardInDeck)
    {
        currentCardIndex = 0;
        DrawNextCard(_cardInDeck);
    }

    public void DrawNextCard(List<CardPrefab> _cardsInDeck)
    {
        if (currentCardIndex < _cardsInDeck.Count)
        {
            if (_cardsInDeck.Count != 0)
            { 
                CardPrefab drawCard = _cardsInDeck[currentCardIndex];
                cardInHand.Add(drawCard);
                _cardsInDeck.RemoveAt(currentCardIndex);
                
                
                VisualCardDrawInHand(drawCard);
            }
            else
                _deckController.ReturnCardInDeck();
            currentCardIndex++;
            StartCoroutine(DelayedDrawNextCard(_cardsInDeck));
        }
        
    }

    private IEnumerator DelayedDrawNextCard(List<CardPrefab> cardPrefabs)
    {
        yield return new WaitForSeconds(0.1f);
        DrawCard(cardPrefabs);
    }
    
    public void Discard(List<CardPrefab>_trashCards)
    {
        while (cardInHand.Count != 0)
        {
            CardPrefab card = cardInHand[0];
            _trashCards.Add(card);
            cardInHand.RemoveAt(0);
        }
    }

    private void VisualCardDrawInHand(CardPrefab drawCard)
    {
      GameObject newCardInHand = Instantiate(drawCard.gameObject, handTransform);

      animator = newCardInHand.GetComponent<Animator>();

      if (animator != null)
         {
            animator.SetBool("IsDrawing", true);
         }
    }
}
