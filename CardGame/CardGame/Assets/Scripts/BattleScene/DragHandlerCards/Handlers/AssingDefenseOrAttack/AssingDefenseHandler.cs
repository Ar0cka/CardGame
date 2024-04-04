using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class AssingDefenseHandler : AbstractAssignAttackAndDefense, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LineManager _lineManager;

    

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isAssigningAttackers)
        AssigningDefense.RemoveDefenser(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isAssigningAttackers)
        {
            _lineManager.ShowIndication();
            isBeginLine = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (zoneTag == enemyTag)
        {
            AssigningDefense.AddNewDefense(gameObject, target);
            _lineManager.OffLine();
            Debug.Log("add new defenser");
        }
        else
          _lineManager.OffLine();
        
        isBeginLine = false;
    }


}
