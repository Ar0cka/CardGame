using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class AssignAttack : AbstractAssignAttackAndDefense, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private LineManager _lineManager;
    
    private bool isAssigningAttackers = false;

    private void FixedUpdate()
    {
        if (isBeginLine)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                Debug.Log(zoneTag + "ZoneTag");
                if (hit.collider != null)
                {
                    zoneTag = hit.collider.tag;
                    target = hit.collider.gameObject;
                    break;
                }
            }
        }
    }
    
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
