using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField] List<CardPrefab> _startCards;
    private TextMeshPro _amountCard;
    private void Awake()
    {
        _amountCard = GetComponentInChildren<TextMeshPro>();
        foreach (var card in _startCards)
        {
            DrawCard();
        }
    }

    private void DrawCard()
    {

    }
}
