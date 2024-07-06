using System.Collections.Generic;
using Deck.InitiallizeObjectPool.Interfase;
using Deck.Shuffle;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

#region summary
// Пул объектов - выполняет функцию базы данных для объектов во время боя
// 1. Создание объектов в пуле
// 2. Взятие неактивных объектов по индексу
// 3. Возврат обьъектов в пул
// 4. Shuffle пул в зависимости от деки
//
//
//
//
//
#endregion
public class InitializeObjectToPool : IReturnObjectToPool, ITakeObjectFromPool, ICreateNewObjectToPool 
{ 
    private RectTransform handTransform;
    
    private List<GameObject> poolHands = new List<GameObject>();
    private List<GameObject> poolBattelZone = new List<GameObject>();
    private GameObject card;
    private int maxSizePool;

    public void GetMaxSizePool(int countDeck)
    {
        maxSizePool = countDeck;
    }
    
    public void CreateNewObjectToPool(CardPrefab cardPref, RectTransform _handTransform)
    {
        handTransform = _handTransform;
        
        if (poolHands.Count < maxSizePool)
        {
            card = GameObject.Instantiate(cardPref.gameObject, handTransform.position, handTransform.rotation, handTransform);
            Debug.Log("Card create");
            card.SetActive(false);
            
            poolHands.Add(card);
        }
    }
    
    public GameObject GetObjectFromPool(GameObject card)
    {
        int index = poolHands.FindIndex(obj => obj.gameObject.GetComponent<CardPrefab>().uniqueID == card.GetComponent<CardPrefab>().uniqueID);
        
            card = poolHands[index];
            card.SetActive(true);
            return card;
    }
    public void ReturnObjectInPool(GameObject objCard)
    {
        int index = poolHands.FindIndex(obj => obj.gameObject == objCard);
        
        card = poolHands[index];
        card.SetActive(false);
    }

    public void CreateObjectToBattelZonePool(CardPrefab _cardPrefab)
    {
        GameObject cardObject = _cardPrefab.gameObject;
        int index = poolHands.FindIndex(obj => obj == cardObject);
        
        if (index != -1)
        {
            poolBattelZone.Add(poolHands[index]);
            poolHands.RemoveAt(index);
        }
    }
    public void ReturnObjectToHandPool(CardPrefab cardPrefab)
    {
        GameObject cardObject = cardPrefab.gameObject;
        int index = poolBattelZone.FindIndex(obj => obj == cardObject);

        if (index != -1)
        {
            poolHands.Add(poolBattelZone[index]);
            poolBattelZone[index].SetActive(false);
            poolBattelZone[index].transform.SetParent(handTransform);
            poolBattelZone.RemoveAt(index);
        }
    }

    public void ShufflePool(IShuffle _shuffle)
    {
       _shuffle.ShuffleDeckAndPool(poolHands);
    }

    public List<GameObject> ReturnPoolHands()
    {
        return poolHands;
    }
}
