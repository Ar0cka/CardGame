using System;
using UnityEngine;


public class DeathToken : MonoBehaviour , IToken
{
    public string nameToken { get; set; }

    [SerializeField] private int damageForOneToken;
    public int _damageForOneToken => damageForOneToken;

    private CardPrefab _cardPrefab;

    private void Awake()
    {
        _cardPrefab = GetComponent<CardPrefab>();
    }

    public void SettingsToken()
    {
        if (_cardPrefab._haveDeathToken)
        {
            
        }
    }    
}
