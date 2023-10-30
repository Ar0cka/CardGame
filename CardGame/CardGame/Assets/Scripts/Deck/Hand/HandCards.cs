using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class HandCards : MonoBehaviour
{
    #region SerilizeParametrs

    [Header("Deck")] [SerializeField] private DeckController _deckController; // ссылка на колоду карт

    [SerializeField] private InitializeObjectToPool _initializeObject; // ссылка на пул объектов

    private List<CardPrefab> cardInHand = new List<CardPrefab>(); // лист хранящий карты, которые находятся в руке
    private List<GameObject> _objectInHand = new List<GameObject>();
    public Transform handTransform; // местоположения хранения карт

    private int maxHandSize = 5; // максимальная велечина объектов

    [Header("Animator")] private Animator animatorDrawCard; // анимацаия добора кар

    #endregion

    public void DrawNextCard(List<CardPrefab> _cardsInDeck, List<int> _allIndexCard,
        List<int> _allIndexObject) // взятие карт
    {
        if (cardInHand.Count != maxHandSize) // условие для добора где происходит проверка есть ли в колоде карты и не заполнена ли рука
        {
            if (_cardsInDeck.Count != 0)
            {
                #region CreateObjectInHand
                
                CardPrefab drawCard = _cardsInDeck[_allIndexCard[0]]; // взятия из листа нужной карты

                cardInHand.Add(drawCard); // добавление в руку

                _objectInHand.Add(_initializeObject.GetObjectFromPool(_allIndexObject[0])); // взятие объекта из пула

                _cardsInDeck.RemoveAt(_allIndexCard[0]); // удаление карты из колоды
                _allIndexCard.RemoveAt(0); // удаление индекса карты
                _allIndexObject.RemoveAt(0); // удаление индекса объекта

                #endregion

                VisualDrawCardInHand(); //визуализация добора

                _deckController.UpdateIndex();
            }
            else
                _deckController.ReturnCardInDeck(); // если не выполнено хоть одно условие, то пересобираем колоду

            StartCoroutine(DelayedDrawNextCard(_cardsInDeck, _allIndexCard,
                _allIndexObject)); // корутина, чтобы анимация добора успела проиграться
        }
    }

    private IEnumerator DelayedDrawNextCard(List<CardPrefab> _cardPrefabs, List<int> _allIndex, List<int> _allIndexObj)
    {
        yield return new WaitForSeconds(0.1f); // длительность добора 1 карты
        DrawNextCard(_cardPrefabs, _allIndex, _allIndexObj); // перевызов метода после взятия карты
    }

    public void Discard(List<CardPrefab> _trashCards) // сброс карты с руки
    {
        while (cardInHand.Count != 0)
        {
            CardPrefab card = cardInHand[0]; // приравневание элемента к CardPrefab
            _trashCards.Add(card); // добавление карты в стопку сброса
            cardInHand.RemoveAt(0); // удаление карты из руки
            
            _initializeObject.ReturnGameObjectToPool(_objectInHand[0]);
            
            VisualDiscardCardFromHand();
        }
    }

    private void VisualDiscardCardFromHand()
    {
        animatorDrawCard = GetComponentInChildren<Animator>();
        try
        {
            animatorDrawCard.SetBool("IsDiscard", true);
        }
        catch (Exception e)
        {
            Debug.LogError("No find");
        }
        
    }

    private void VisualDrawCardInHand()
    {
        animatorDrawCard = GetComponentInChildren<Animator>();
        
        try
        {
            animatorDrawCard.SetBool("IsDraw", true);
        }
        catch (Exception e)
        {
            Debug.LogError("No find");
        }
    }
}
