using System;
using UnityEngine;


public class AbstractAttackHandlers : MonoBehaviour
{
    protected string zoneTag;
    protected bool isBeginLine;

    protected GameObject target;
    
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
}
