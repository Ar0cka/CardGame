using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;


public class HendlerCardsInBattleZone : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CardPrefab cardPrefab;

    private string zoneTag;

    private HendlerCardsInBattleZone _hendler;

    private DropCardInPanel _dropCard;
    private InitializeObjectToPool _objectToPool;

    private bool cardsTypes;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        cardPrefab = GetComponent<CardPrefab>();

        _hendler = GetComponent<HendlerCardsInBattleZone>();
        
        _dropCard = FindObjectOfType<DropCardInPanel>();
        _objectToPool = FindObjectOfType<InitializeObjectToPool>(); 

        #region BoolCards

        cardsTypes = cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.AttackHuman
                     || cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.AttackRangeHuman
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
            if (zoneTag == "BattleZone" && cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.AttackHuman)
            _dropCard.ChangeLane(zoneTag, cardPrefab);
            transform.SetParent(_dropCard.battleZone.transform);
        }
        else
        {
            transform.SetParent(_dropCard.ReturnCurrentZone(cardPrefab.currentZoneTag));
        }
    }
}
                                                                                            