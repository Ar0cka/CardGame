using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    public string uniqueID;
    
    [SerializeField] private Image _iconCard;
    [SerializeField] public CardInfo _cardInfo;

    private void Awake()
    {
        uniqueID = Guid.NewGuid().ToString();
        
        if (_cardInfo != null)
        _iconCard.sprite = _cardInfo.iconCard;
    }
}
