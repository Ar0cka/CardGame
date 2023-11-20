using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;


public class HendlerCards : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _handTransform;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CardPrefab cardPrefab;

    private RaycastHit2D hit;
    private Camera _camera;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        cardPrefab = GetComponent<CardPrefab>();

        _handTransform = GetComponentInParent<Transform>();
        _camera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
            LayerMask.GetMask("BattleZoneLayer"));
        
        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward * 10f, Color.red, 1f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (hit.collider != null)
        {
            string zoneTag = hit.collider.tag;
            var typeCard = cardPrefab._cardInfo.type;
            
             switch (zoneTag)
             {
                 case "MiliArmy":
                     if (typeCard == CardInfo.TypeCard.AttackHuman || typeCard == CardInfo.TypeCard.DefenseBuild)
                     {
                         // Обработка для карт с типом AttackHuman и DefenseBuild в зоне MiliArmy
                         Debug.Log("Dropped AttackHuman or DefenseBuild in MiliArmy");
                     }
                     else
                     {
                         _rectTransform.anchoredPosition = _handTransform.position;
                         Debug.Log("Invalid card type for MiliArmy");
                     }
                     break;
            
                 case "RangeSolder":
                     if (typeCard == CardInfo.TypeCard.AttackRangeHuman)
                     {
                         // Обработка для карт с типом AttackRangeHuman в зоне RangeSolder
                         Debug.Log("Dropped AttackRangeHuman in RangeSolder");
                     }
                     else
                     {
                         _rectTransform.anchoredPosition = _handTransform.position;
                         Debug.Log("Invalid card type for RangeSolder");
                     }
                     break;
            
                 case "RangeBuild":
                     if (typeCard == CardInfo.TypeCard.AttackRangeBuild || typeCard == CardInfo.TypeCard.AuxiliaryBuild)
                     { 
                         // Обработка для карт с типом AttackRangeBuild и AuxiliaryBuild в зоне RangeBuild
                         Debug.Log("Dropped AttackRangeBuild or AuxiliaryBuild in RangeBuild");
                     }
                     else
                     {
                         _rectTransform.anchoredPosition = _handTransform.position;
                         Debug.Log("Invalid card type for RangeBuild");
                     } 
                     break;
             }
        }
        else
        {
            _rectTransform.anchoredPosition = _handTransform.position;
            Debug.Log("HitColider = null");
        }
    }
}
                                                                                            