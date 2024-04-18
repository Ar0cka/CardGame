using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HandlerController : MonoBehaviour
{
    private bool isTable;

    public bool _isTable => isTable;
    
    [SerializeField] private HandlerCardsInTableFromHand _handlerFromHand;
    [SerializeField] private HandlerCardsInBattleZone _handlerSwitchZone;
    [SerializeField] private HandlerCardsFromBattleZone _handlerCardsFromBattle;
    [SerializeField] private AssignAttackHandler assignAttackHandlerAndDefense;
    [SerializeField] private AssingDefenseHandler assingDefenseHandler;

    private CardPrefab _cardPrefab;

    private void Awake()
    {
        isTable = false;

        #region SettingsHandlers

        _handlerFromHand.enabled = false;
        _handlerSwitchZone.enabled = false;
        _handlerCardsFromBattle.enabled = false;
        assignAttackHandlerAndDefense.enabled = false;
        assingDefenseHandler.enabled = false;
        
        #endregion
    }

    public void CardInTable()
    {
        isTable = true;
        _handlerFromHand.enabled = false;
    }

    public void RemoveCardFromTable()
    {
        isTable = false;
        _handlerFromHand.enabled = true;
    }

    public void OnHandlersFromHand()
    {
        _handlerFromHand.enabled = true;
    }
    
    public void SettingsHandlersFromHand(bool phase)
    {
        if (!phase || isTable)
        {
            _handlerFromHand.enabled = false;
        }
        else if (!isTable && phase)
        {
            _handlerFromHand.enabled = true;
        }
    }

    #region ChangeHandler

    public void OnSwitchHandler()
    {
        _handlerSwitchZone.enabled = true;
        _handlerCardsFromBattle.enabled = false;
    }

    public void OnHandlerFromBattleZone()
    {
        _handlerSwitchZone.enabled = false;
        _handlerCardsFromBattle.enabled = true;
    }
    
    #endregion

    public void BeginAttackPhase()
    {
        assignAttackHandlerAndDefense.enabled = false;
    }

    public void BeginBattleFase()
    {
        _handlerSwitchZone.enabled = false;
        _handlerCardsFromBattle.enabled = false;
        assignAttackHandlerAndDefense.enabled = true;
    }

    public void DefenseHandlerConroller(bool isDefensePhase)
    {
        if (isDefensePhase)
            assingDefenseHandler.enabled = true;
        else
            assingDefenseHandler.enabled = false;
    }
}
