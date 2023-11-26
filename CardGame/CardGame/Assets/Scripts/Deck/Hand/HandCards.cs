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

    private int maxSizeHand = 5;
    
    public void DrawCard(List<CardPrefab> _deckList)
    {
        StartCoroutine(DrawNextCard(_deckList));
    }
    
    private IEnumerator DrawNextCard(List<CardPrefab> _deckList)
    {
        int i = 0;
        while(_cardInHand.Count < maxSizeHand)
        {
            if (_deckList.Count != 0)
            {
                CardPrefab card = _deckList[0];
                _cardInHand.Add(card);
                
                _objectPool.Add(_initializeObject.GetObjectFromPool(i));
                
                _animators.Add(_objectPool[i].GetComponentInChildren<Animator>());
                
                _deckList.RemoveAt(0);
                
                VisualDrawCard(i);

                yield return new WaitForSeconds(0.5f);

                i++;
                
                _deckController.UpdateUIDeck(_deckList);
            }
            else
                _deckController.ReturnDeck();
        }
    }

    public void DiscardCard(List<CardPrefab> _discardDeck)
    {
        StartCoroutine(Discard(_discardDeck));
    }
    
    private IEnumerator Discard(List<CardPrefab> _discardDeck)
    {
        for (int i = _cardInHand.Count - 1; i >= 0; i--)
        {
            CardPrefab card = _cardInHand[i];
            
            _discardDeck.Add(card);
            
            VisualDiscardCard(i);
            
            yield return new WaitForSeconds(0.5f);
            
            _initializeObject.ReturnObjectInPool(_objectPool[i]);
            
            _cardInHand.RemoveAt(i);
            _objectPool.RemoveAt(i);
            _animators.RemoveAt(i);
            
            _deckController.UpdateUIDiscardDeck(_discardDeck);
        }
    }

    public void DropCardFromHand(string uniqueID)
    {
        int index = _cardInHand.FindIndex(card => card.uniqueID == uniqueID);
        _cardInHand.RemoveAt(index);
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
