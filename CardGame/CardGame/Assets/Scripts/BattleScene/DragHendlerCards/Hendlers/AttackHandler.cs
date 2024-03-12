using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class AttackHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Color _color;
    [FormerlySerializedAs("_lineRenderer")] [SerializeField] private LineManager _lineManager;
    
    private string zoneTag;

    private GameObject attacker;
    private GameObject target;

    private void FixedUpdate()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        int ignoreLayer = 1 << LayerMask.NameToLayer("Ignore Raycast");
        int allRaycastExceptIgnore = ~ignoreLayer;
        
        foreach (RaycastResult result in results)
        {
            if (((1 << result.gameObject.layer) & allRaycastExceptIgnore) != 0)
            {
                zoneTag = result.gameObject.tag;
                target = result.gameObject;
                break;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lineManager.ShowIndication();
        attacker = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (zoneTag == "enemy")
        {
            AssigningAttackers.AddAttackerAndTarget(attacker, target);
            _lineManager.OffLine();
        }
        else
        {
            _lineManager.OffLine();
        }
        
    }
}
