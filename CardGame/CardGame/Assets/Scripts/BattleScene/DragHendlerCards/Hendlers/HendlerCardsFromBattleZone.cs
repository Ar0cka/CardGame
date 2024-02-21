using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;


public class HendlerCardsFromBattleZone : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CardPrefab cardPrefab;

    private string zoneTag;

    private HendlerCardsFromBattleZone _hendler;

    private DropCardInPanel _dropCard;

    private bool cardsTypes;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        cardPrefab = GetComponent<CardPrefab>();

        _hendler = GetComponent<HendlerCardsFromBattleZone>();
        
        _dropCard = FindObjectOfType<DropCardInPanel>();

        #region BoolCards

        cardsTypes = cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.AttackHuman
                     || cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.DefenseBuild;

        #endregion
        
        _hendler.enabled = false;
    }

    private void Update()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            // Проверяем слой объекта
            if (result.gameObject.layer == LayerMask.NameToLayer("BattleZoneLayer"))
            {
                zoneTag = result.gameObject.tag;
                break; 
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cardsTypes)
        transform.SetParent(_dropCard._hendlerZone.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (zoneTag != null)
        {
            if (zoneTag == "MiliArmy" && cardsTypes) 
                _dropCard.ChangeLane(zoneTag, cardPrefab);
            transform.SetParent(_dropCard.miliArmyZone.transform);
        }
        else
        {
            transform.SetParent(_dropCard.ReturnCurrentZone(cardPrefab.currentZoneTag));
        }
    }
}
                                                                                            