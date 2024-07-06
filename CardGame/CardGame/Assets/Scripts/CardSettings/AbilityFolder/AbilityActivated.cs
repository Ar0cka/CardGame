using System.Collections;
using System.Collections.Generic;
using CardSettings.AbilityFolder.Settings;
using UnityEngine;

public class AbilityActivated : MonoBehaviour
{
    [SerializeField] private MonoBehaviour scriptsAbility;
    private CardPrefab _cardPref;

    private void Awake()
    {
        _cardPref = GetComponent<CardPrefab>();
    }

    public void ActivateAbility()
    {
        if (scriptsAbility is IAbility)
        {
            (scriptsAbility as IAbility).ActivatedAbility(_cardPref._cardInfo);
        }

        if (scriptsAbility is ITokenEffect)
        {
            (scriptsAbility as ITokenEffect).ActivatingAppoint();
        }
    }
}
