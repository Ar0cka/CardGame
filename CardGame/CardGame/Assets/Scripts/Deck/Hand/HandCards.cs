using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

#region summary
// Данный скрипт отвечает за работу карт в руке
// 1. Метод для взятия карт в руку
// 2. Метод для дискарда карт
// 3. Метод для визуализации добора и сброса карт
//
//
//
#endregion
public class HandCards : MonoBehaviour
{
    [SerializeField] private DeckController _deckController;
    [SerializeField] private InitializeObjectToPool _initializeObject;
    
    private List<CardPrefab> _cardInHand = new List<CardPrefab>();
    private List<GameObject> _objectPool = new List<GameObject>();
    private List<Animator> _animators = new List<Animator>();

    private int counterHandCards = 0;

    private HandlerController _handlerController;

    private int maxSizeHand = 5;
    
    public void DrawCard(List<CardPrefab> _deckList, List<CardPrefab> _discardList)
    {
        if (_deckList.Count > 0 || _discardList.Count > 0)  
        StartCoroutine(DrawNextCard(_deckList, _discardList));
    }
    
    private IEnumerator DrawNextCard(List<CardPrefab> _deckList, List<CardPrefab> _discardList)
    {
        while(_cardInHand.Count < maxSizeHand && (_deckList.Count != 0 || _discardList.Count != 0) && counterHandCards < maxSizeHand)
        {
            if (_deckList.Count != 0)
            {
                CardPrefab card = _deckList[0];
                _cardInHand.Add(card);
                
                _objectPool.Add(_initializeObject.GetObjectFromPool(card.gameObject));
                
                _deckList.RemoveAt(0);

                yield return new WaitForSeconds(0.2f);

                counterHandCards++;
                
                _deckController.UpdateUIDeck(_deckList);
            }
            else
                _deckController.ReturnDeck();
        }

        counterHandCards = 0;
        
        foreach (var card in _cardInHand)
        {
            var handlerController = card.GetComponent<HandlerController>();
            handlerController.OnHandlersFromHand();
        }
    }

    public void DiscardCard(List<CardPrefab> _discardDeck)
    {
        if (_cardInHand.Count > 0)
        StartCoroutine(Discard(_discardDeck));
    }
    
    private IEnumerator Discard(List<CardPrefab> _discardDeck)
    {
        for (int i = _cardInHand.Count - 1; i >= 0; i--)
        {
            CardPrefab card = _cardInHand[i];
            
            _discardDeck.Add(card);
            
            yield return new WaitForSeconds(0.5f);
            
            _initializeObject.ReturnObjectInPool(_objectPool[i]);
            
            _cardInHand.RemoveAt(i);
            _objectPool.RemoveAt(i);
            
            _deckController.UpdateUIDiscardDeck(_discardDeck);
        }
    }

    public void DropCardFromHand(CardPrefab cardPrefab, string uniqueID)
    {
        _handlerController = cardPrefab.GetComponent<HandlerController>();
        if (_handlerController._isTable == false)
        {
            int index = _cardInHand.FindIndex(card => card.uniqueID == uniqueID);
            _cardInHand.RemoveAt(index);
        
            GameObject cardObject = cardPrefab.gameObject;
            int indexObject = _objectPool.FindIndex(obj => obj == cardObject);
            _objectPool.RemoveAt(indexObject);
        }
    }

    public void repackHandList(List<CardPrefab> cardsList)
    {
        foreach (var cards in _cardInHand)
        {
           cardsList.Add(cards); 
        }
    }

    private void VisualDrawCard(int index)
    {
        _animators[index].SetTrigger("IsDraw");
    }

    private void VisualDiscardCard(int index)
    {
        _animators[index].SetTrigger("IsDiscard");
    }
}
