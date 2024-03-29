using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class AssingDefense : AbstractAssignAttackAndDefense, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LineManager _lineManager;

    

    public void OnPointerClick(PointerEventData eventData)
    {
        AssigningDefense.RemoveDefenser(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _lineManager.ShowIndication();
        isBeginLine = true;
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
