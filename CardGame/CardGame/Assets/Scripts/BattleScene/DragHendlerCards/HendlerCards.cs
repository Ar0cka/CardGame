using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class HendlerCards : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _handTransform;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CardPrefab cardPrefab;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        cardPrefab = GetComponent<CardPrefab>();

        _handTransform = GetComponentInParent<Transform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        var typeCard = cardPrefab._cardInfo.type;
        
        if (hit.collider != null && hit.collider.CompareTag("BattleZone"))
        {
            switch (typeCard)
            {
                case CardInfo.TypeCard.AttackHuman:
                    break;
                case CardInfo.TypeCard.DefenseBuild:
                    break;
                case CardInfo.TypeCard.AuxiliaryBuild:
                    break;
                case CardInfo.TypeCard.AttackRangeBuild:
                    break;
                case CardInfo.TypeCard.AttackRangeHuman:
                    break;
            }
        }
        else
        {
            _rectTransform.anchoredPosition = _handTransform.position;
        }
    }
}
                                                                                            