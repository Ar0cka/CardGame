using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaCardsPrefab : MonoBehaviour
{
    public string uniqueID;
    
    [SerializeField] private Image _iconCard;
    [SerializeField] public ResursBuild _cardInfo;
    
    private void Start()
    {
        uniqueID = Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        if (_cardInfo != null)
            _iconCard.sprite = _cardInfo.iconCard;
    }
    
    
}
