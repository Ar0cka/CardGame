using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
public class InitializeObjectToPool : MonoBehaviour
{
    [SerializeField] public RectTransform handTransform;
    [SerializeField] private DeckController _deck;
    
    
    public List<GameObject> poolHands = new List<GameObject>();
    [FormerlySerializedAs("poolBattelAZone")] public List<GameObject> poolBattelZone;
    private GameObject card;
    private int maxSizePool;

    public void GetMaxSizePool()
    {
        maxSizePool = _deck.RetunrCountDeck();
    }
    
    public void CreateNewObjectToPool(CardPrefab cardPref)
    {
        if (poolHands.Count < maxSizePool)
        {
            card = Instantiate(cardPref.gameObject, handTransform);
            card.SetActive(false);
            poolHands.Add(card);
        }
    }
    
    public GameObject GetObjectFromPool(int index)
    {
        if (poolHands != null)
        {
            card = poolHands[index];
            card.SetActive(true);
            return card;
        }
        else
            return null;
    }
    public void ReturnObjectInPool(GameObject obj)
    {
        obj.SetActive(false);
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
            poolBattelZone.RemoveAt(index);
        }
    }
}
