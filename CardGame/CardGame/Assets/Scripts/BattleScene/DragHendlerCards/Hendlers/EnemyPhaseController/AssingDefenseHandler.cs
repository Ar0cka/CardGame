using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class AssingDefenseHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private CardPrefab _cardPrefab;
    private DropCardInPanel _drop;
    private LineManager _lineManager;

    private bool isDefense = false;

    private void Awake()
    {
        _cardPrefab = GetComponent<CardPrefab>();
        _lineManager = GetComponent<LineManager>();
        _drop = FindObjectOfType<DropCardInPanel>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _lineManager.ShowIndication();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void CardDead()
    {
        _drop.RemoveCardFromBattleZone(_cardPrefab.uniqueID);
    }
    
    public void AttackEnemy(GameObject attacker)
    {
        AssigningDefense.AddNewDefense(gameObject, attacker);
    }
}
