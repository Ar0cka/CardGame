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

    private void Awake()
    {
        isTable = false;

        #region SettingsHandlers

        _handlerSwitchZone.enabled = false;
        _handlerCardsFromBattle.enabled = false;

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

    #region SwitchHandler

    public void OnSwitchHandler()
    {
        _handlerSwitchZone.enabled = true;
    }

    public void OffSwitchHandler()
    {
        _handlerSwitchZone.enabled = false;
    }

    #endregion

    #region SettingsHandlerFromBattleZone

    public void OnHandlerCardsFromBattleZone()
    {
        _handlerCardsFromBattle.enabled = true;
    }
    
    public void OffHandlerCardsFromBattle()
    {
        _handlerCardsFromBattle.enabled = false;
    }

    #endregion
    
    #region HendlersFromHand

    public void OffHandlersFromHand()
    {
        _handlerFromHand.enabled = false;
    }

    public void OnHandlersFromHand()
    {
        _handlerFromHand.enabled = true;
    }

    #endregion
    

    
}
