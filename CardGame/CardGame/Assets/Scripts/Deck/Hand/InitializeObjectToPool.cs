using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Transform handTransform;
    public List<GameObject> pool = new List<GameObject>(); 
    private GameObject card;
    private int maxSizePool = 10;
    
    public void CreateNewObjectToPool(CardPrefab cardPref)
    {
        if (pool.Count < maxSizePool)
        {
            card = Instantiate(cardPref.gameObject, handTransform);
            card.SetActive(false);
            pool.Add(card);
        }
    }

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
    }
    public void ReturnObjectInPool(GameObject obj)
    {
        obj.SetActive(false);
    }

 

}
