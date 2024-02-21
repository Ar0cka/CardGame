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

    [HideInInspector]
    public string currentZoneTag;
        
    private void Start()
    {
        uniqueID = Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        if (cardInfo != null)
        _iconCard.sprite = cardInfo.iconCard;
    }

    public void SetZoneTag(string zone)
    {
        currentZoneTag = zone;
    }
    

    
    
}
