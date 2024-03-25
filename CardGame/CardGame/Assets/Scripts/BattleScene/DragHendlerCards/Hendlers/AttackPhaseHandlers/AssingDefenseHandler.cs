using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class AssingDefenseHandler : AbstractAttackHandlers, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LineManager _lineManager;
    
    private CardPrefab _cardPrefab;
    private DropCardInPanel _drop;
    
    private bool isDefense = false;

    private void Awake()
    {
        _cardPrefab = GetComponent<CardPrefab>();
        _lineManager = GetComponent<LineManager>();
        _drop = FindObjectOfType<DropCardInPanel>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AssigningDefense.RemoveDefenser(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _lineManager.ShowIndication();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (zoneTag == "Enemy")
        {
            AssignDefensers(target);
        }
    }

    public void CardRemoveFromBattleZone()
    {
        _drop.RemoveCardFromBattleZone(_cardPrefab.uniqueID);
    }
    
    public void AssignDefensers(GameObject target)
    {
        AssigningDefense.AddNewDefense(gameObject, target);
    }
}
