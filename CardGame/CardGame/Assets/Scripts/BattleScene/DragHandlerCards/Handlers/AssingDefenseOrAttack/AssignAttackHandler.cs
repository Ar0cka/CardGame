using System;
using System.Collections.Generic;
using CardSettings.CardPrefabSettings;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class AssignAttackHandler : AbstractAssignAttackAndDefense, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private LineManager _lineManager;
    [SerializeField] private TokenInCardSystem _tokenInCardSystem;
    
    public void OffAssigningAttackers()
    {
        isAssigningAttackers = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isAssigningAttackers)
        {
            AssigningAttackers.RemoveAttacker(gameObject, target);
            OffAssigningAttackers();
            Debug.Log("OffAttack");
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isAssigningAttackers && _tokenInCardSystem.canAttack)
        {
            isBeginLine = true;
            _lineManager.ShowIndication();
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (zoneTag == enemyTag)
        {
            AssigningAttackers.AddAttackerAndTarget(gameObject, target);
            _lineManager.OffLine();
            isAssigningAttackers = true;
            zoneTag = "";
            Debug.Log("metod complite");
        }
        else
        {
            _lineManager.OffLine();
        }
        isBeginLine = false;
    }
}
