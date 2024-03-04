using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HendlerController : MonoBehaviour
{
    private bool isTable;

    public bool _isTable => isTable;
    
    [FormerlySerializedAs("hendlerFromHand")] [SerializeField] private HandlerCardsInTableFromHand _hendlerFromHand;
    [SerializeField] private HandlerCardsInBattleZone _hendlerSwitchZone;

    private void Awake()
    {
        isTable = false;
    }

    public void CardInTable()
    {
        isTable = true;
        _hendlerFromHand.enabled = false;
    }

    public void RemoveCardFromTable()
    {
        isTable = false;
        _hendlerFromHand.enabled = true;
    }

    public void PenutationPhase()
    {
        _hendlerSwitchZone.enabled = true;
    }

    #region HendlersFromHand

    public void OffHendlersFromHand()
    {
        _hendlerFromHand.enabled = false;
    }

    public void OnHendlersFromHand()
    {
        _hendlerFromHand.enabled = true;
    }

    #endregion
    

    public void endPenutationPhase()
    {
        _hendlerSwitchZone.enabled = false;
    }
}
