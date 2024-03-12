using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    public string uniqueID;
    
    [SerializeField] private Image _iconCard;
    [SerializeField] private CardInfo cardInfo;
    public CardInfo _cardInfo => cardInfo;
    private EnemyController _enemyController;

    [HideInInspector]
    public string currentZoneTag;

    private void Awake()
    {
        if (cardInfo != null)
        _iconCard.sprite = cardInfo.iconCard;
        
        uniqueID = Guid.NewGuid().ToString();

        _enemyController = FindObjectOfType<EnemyController>();
    }

    public void SetZoneTag(string zone)
    {
        currentZoneTag = zone;
    }

    public void DealDamage()
    {
        _enemyController.AttackEnemy(_cardInfo.damage);
    }
    

    
    
}
