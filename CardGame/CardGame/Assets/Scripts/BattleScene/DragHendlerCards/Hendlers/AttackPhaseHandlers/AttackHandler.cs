using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class AttackHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private Color _color;
    [FormerlySerializedAs("_lineRenderer")] [SerializeField] private LineManager _lineManager;
    
    private string zoneTag;

    private GameObject attacker;
    private GameObject target;

    private bool beginLine = false;
    private bool isAssigningAttackers = false;

    private void FixedUpdate()
    {
        if (beginLine)
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
            beginLine = true;
            _lineManager.ShowIndication();
            attacker = gameObject;
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (zoneTag == "enemy")
        {
            AssigningAttackers.AddAttackerAndTarget(attacker, target);
            _lineManager.OffLine();
            isAssigningAttackers = true;
            zoneTag = "";
            Debug.Log("metod complite");
        }
        else
        {
            _lineManager.OffLine();
        }
        beginLine = false;
    }
}
