using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HendlerController : MonoBehaviour
{
    private bool isTable;

    public bool _isTable => isTable;
    
    [FormerlySerializedAs("hendlerFromHand")] [SerializeField] private HendlerCardsInTableFromHand _hendlerFromHand;
    [SerializeField] private HendlerCardsInBattleZone _hendlerSwitchZone;

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

    public void OffHendlersFromHand()
    {
        _hendlerFromHand.enabled = false;
    }

    public void OnHendlersFromHand()
    {
        _hendlerFromHand.enabled = true;
    }

    public void endPenutationPhase()
    {
        _hendlerSwitchZone.enabled = false;
    }
}
