using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;


public class HendlerCards : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CardPrefab cardPrefab;

    private string zoneTag;

    private DropCardInPanel _dropCard;
    private InitializeObjectToPool _objectToPool;
    private HendlerCards _hendlerCards;

    private AbilityActivated _abilityActivated;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        cardPrefab = GetComponent<CardPrefab>();
        _hendlerCards = GetComponent<HendlerCards>();
        _abilityActivated = GetComponent<AbilityActivated>();
        
        _dropCard = FindObjectOfType<DropCardInPanel>();
        _objectToPool = FindObjectOfType<InitializeObjectToPool>();
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
            var typeCard = cardPrefab._cardInfo.subtype;
            
             switch (zoneTag)
             {
                 case "MiliArmy":
                     if (typeCard == CardInfo.SubtypeCard.AttackHuman || typeCard == CardInfo.SubtypeCard.DefenseBuild)
                     {
                         _dropCard.DropNewCardInPanel( cardPrefab, zoneTag);
                         _hendlerCards.enabled = false;
                         transform.SetParent(_dropCard.miliArmyZone.transform);
                         
                         if (_abilityActivated != null)
                        _abilityActivated.ActivateAbility();
                     }
                     else
                     {
                         transform.SetParent(_objectToPool.handTransform.transform);
                     }
                     break;
            
                 case "RangeSolder":
                     if (typeCard == CardInfo.SubtypeCard.AttackRangeHuman)
                     {
                         _dropCard.DropNewCardInPanel(cardPrefab, zoneTag);
                         _hendlerCards.enabled = false;
                         transform.SetParent(_dropCard.rangeArmyZone.transform);
                         
                         if (_abilityActivated != null)
                        _abilityActivated.ActivateAbility();
                    }
                     else
                     {
                         transform.SetParent(_objectToPool.handTransform.transform);
                     }
                     break;
            
                 case "RangeBuild":
                     if (typeCard == CardInfo.SubtypeCard.AttackRangeBuild || typeCard == CardInfo.SubtypeCard.AuxiliaryBuild)
                     { 
                         _dropCard.DropNewCardInPanel(cardPrefab, zoneTag);
                         _hendlerCards.enabled = false;
                         transform.SetParent(_dropCard.rangeBuildZone.transform);
                         
                         if (_abilityActivated != null)
                        _abilityActivated.ActivateAbility();
                    }
                     else
                     {
                         transform.SetParent(_objectToPool.handTransform.transform);
                     } 
                     break;
             }
        }
        else
        {
            transform.SetParent(_objectToPool.handTransform.transform);
        }
    }
}
                                                                                            