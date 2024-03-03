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

    private Vector3 originalPosition;
    private Transform originalParent;
    private GameObject _gameCardEmpety;
    
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
                     || cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.DefenseBuild 
                     || cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.AttackRangeHuman;

        #endregion
        
        _hendler.enabled = false;
        _gameCardEmpety.SetActive(false);
    }

    private void FixedUpdate()
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.position;

        _gameCardEmpety.SetActive(true);
        _gameCardEmpety.transform.position = originalPosition;
        
        _gameCardEmpety.transform.SetParent(_dropCard.ReturnCurrentZone(cardPrefab.currentZoneTag));
        
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
            _gameCardEmpety.SetActive(false);
        }
        else
        {
            transform.SetParent(_dropCard.ReturnCurrentZone(cardPrefab.currentZoneTag));
            transform.position = originalPosition;
            _gameCardEmpety.SetActive(false);
        }
    }
}
                                                                                            