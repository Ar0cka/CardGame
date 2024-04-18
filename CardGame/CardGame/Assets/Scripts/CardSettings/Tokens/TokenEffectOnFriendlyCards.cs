using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TokenEffectOnFriendlyCards
{
    private Dictionary<CardPrefab, CardPrefab> _fearTokens = new Dictionary<CardPrefab, CardPrefab>(); //Key это карта, на которую добавляется токен, а Value это карта которая накладывает Token
    private Dictionary<CardPrefab, CardPrefab> _lifeTokens = new Dictionary<CardPrefab, CardPrefab>();

    public void AddNewTokenOnFriendCard(CardPrefab tokenApplicatorCard, CardPrefab tokenAffectedCard)
    {
        if (tokenApplicatorCard._haveFearToken)
        {
            _fearTokens.Add(tokenAffectedCard, tokenApplicatorCard);
        }
        else if (tokenApplicatorCard._haveLifeToken)
        {
            _lifeTokens.Add(tokenAffectedCard, tokenApplicatorCard);
        }
    }

    public void ActionFearTokens()
    {
        
    }

    public void RemoveFearTokensFromTarget(CardPrefab targetCard)
    {
        _fearTokens.Remove(targetCard);
    }
}
