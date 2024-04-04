using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class AssignAttackHandler : AbstractAssignAttackAndDefense, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private LineManager _lineManager;
    
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
        if (!isAssigningAttackers)
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
