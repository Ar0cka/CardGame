using System;
using UnityEngine;

public class HendlerController : MonoBehaviour
{
    private bool isTable;

    public bool _isTable => isTable;
    
    [SerializeField] private HendlerCardsInTableFromHand hendlerFromHand;
    [SerializeField] private HendlerCardsInBattleZone _hendlerSwitchZone;

    private void Awake()
    {
        isTable = false;
    }

    public void CardInTable()
    {
        isTable = true;
        hendlerFromHand.enabled = false;
    }

    public void RemoveCardFromTable()
    {
        isTable = false;
        hendlerFromHand.enabled = true;
    }

    public void PenutationPhase()
    {
        _hendlerSwitchZone.enabled = true;
    }

    public void endPenutationPhase()
    {
        _hendlerSwitchZone.enabled = false;
    }
}
