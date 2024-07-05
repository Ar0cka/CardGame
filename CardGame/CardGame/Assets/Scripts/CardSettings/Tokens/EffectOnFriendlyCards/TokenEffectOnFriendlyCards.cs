using System;
using System.Collections.Generic;
using System.Linq;
using CardSettings.CardPrefabSettings;
using CardSettings.Tokens.EffectOnFriendlyCards;
using UnityEngine;

public class TokenEffectOnFriendlyCards : IEffectOnFriendlyCard
{
    private List<CardPrefab> _fearTokens = new List<CardPrefab>();

    public void AddNewTokenInFriendCard(CardPrefab tokenAffectedCard)
    {
        _fearTokens.Add(tokenAffectedCard);
    }

    public void ActionFearToken()
    {
        // Вызвать активацию метода на определенной карте.
        foreach (var token in _fearTokens)
        {
            var tokenSystem = token.GetComponent<TokenInCardSystem>();
            
            tokenSystem.CardHaveFearToken();
        }
    }
    
    public void RemoveFearTokensFromTarget(CardPrefab targetCard)
    {
        _fearTokens.RemoveAt(0);
    }
}
