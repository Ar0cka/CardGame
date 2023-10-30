using System;
using System.Collections.Generic;
using UnityEngine;
public class InitializeObjectToPool : MonoBehaviour
{
    private GameObject card;

    #region SerializeLists

    [SerializeField] private DeckController _deckController;
    
    private List<GameObject> pool = new List<GameObject>();

    #endregion
    
    #region SizePool

    private int _maxPoolSize = 10;
    public int maxPoolSize => _maxPoolSize;

    #endregion 
    
    public void CreateNewObjectToPoll(CardPrefab cards, Transform transformHand)
    {
        if (pool.Count < maxPoolSize)
        {
            card = Instantiate(cards.gameObject, transformHand); 
            card.SetActive(false);
            pool.Add(card);
        }
        
    }// метод для создания нового объекта в пуле

    public GameObject GetObjectFromPool(int index)
    {
        if (pool != null)
        { 
            card = pool[index];
            card.SetActive(true);
            return card;
        }
        else
            return null;
    } // взятие объекта из пула

    public void ReturnGameObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    } // возвращение олбъекта в пул

    public void ToExpandPoolSize() // увелечение максимального размера пула
    {
        _maxPoolSize++;
    }

    public void ShufflePool()
    {
        _deckController.ShuffleList(pool);
    }
}
