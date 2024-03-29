using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbstractHandler : MonoBehaviour
{
    [SerializeField] protected HandlerController _handlerController;
    
    protected RectTransform _rectTransform;
    protected Canvas _canvas;
    protected CardPrefab cardPrefab;
    protected DropCardInPanel _dropCard;
    
    protected string zoneTag;

    protected bool isBeginDrag;
    
    private void FixedUpdate()
    {
        if (isBeginDrag)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);
        
            int ignoreLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
            int allLayersExceptIgnore = ~ignoreLayerMask;

            foreach (RaycastResult result in results)
            {
                if (((1 << result.gameObject.layer) & allLayersExceptIgnore) != 0)
                {
                    zoneTag = result.gameObject.tag;
                    break;
                }
            }
        }
    }     
}
