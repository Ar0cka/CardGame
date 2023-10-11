using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    [SerializeField] private Image _iconCard;
    [SerializeField] private CardInfo _cardInfo;

    private void Start()
    {
        if (_cardInfo != null)
        _iconCard.sprite = _cardInfo.iconCard;
    }
}
