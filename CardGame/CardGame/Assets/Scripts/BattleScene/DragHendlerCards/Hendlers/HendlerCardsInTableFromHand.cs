using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;


public class HandlerCardsInTableFromHand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private HendlerController _hendlerController;
    
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CardPrefab cardPrefab;

    private string zoneTag;

    private DropCardInPanel _dropCard;
    private InitializeObjectToPool _objectToPool;
    private HandlerCardsInTableFromHand _hendlerCardsInTableFromHand;

    private AbilityActivated _abilityActivated;

    private PlayerBattleScene _player;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        cardPrefab = GetComponent<CardPrefab>();
        _hendlerCardsInTableFromHand = GetComponent<HandlerCardsInTableFromHand>();
        _abilityActivated = GetComponent<AbilityActivated>();
        
        _dropCard = FindObjectOfType<DropCardInPanel>();
        _objectToPool = FindObjectOfType<InitializeObjectToPool>();
        _player = FindObjectOfType<PlayerBattleScene>();
    }

    private void Update()
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
                     if (typeCard == CardInfo.SubtypeCard.AttackHuman && _player.manaHuman >= cardPrefab._cardInfo.cost 
                         || typeCard == CardInfo.SubtypeCard.DefenseBuild && _player.manaBuild >= cardPrefab._cardInfo.cost)
                     {
                         _dropCard.DropNewCardInPanel( cardPrefab, zoneTag);
                         _hendlerCardsInTableFromHand.enabled = false;
                         
                         transform.SetParent(_dropCard.miliArmyZone.transform);
                         _hendlerController.CardInTable();
                         
                         if (_abilityActivated != null)
                        _abilityActivated.ActivateAbility();
                     }
                     else
                     {
                         transform.SetParent(_objectToPool.handTransform.transform);
                     }
                     break;
            
                 case "RangeSolder":
                     if (typeCard == CardInfo.SubtypeCard.AttackRangeHuman && _player.manaHuman >= cardPrefab._cardInfo.cost)
                     {
                         _dropCard.DropNewCardInPanel(cardPrefab, zoneTag);
                         _hendlerCardsInTableFromHand.enabled = false;
                         transform.SetParent(_dropCard.rangeArmyZone.transform);
                         _hendlerController.CardInTable();
                         if (_abilityActivated != null)
                        _abilityActivated.ActivateAbility();
                    }
                     else
                     {
                         transform.SetParent(_objectToPool.handTransform.transform);
                     }
                     break;
            
                 case "RangeBuild":
                     if (typeCard == CardInfo.SubtypeCard.AttackRangeBuild || typeCard == CardInfo.SubtypeCard.AuxiliaryBuild && _player.manaBuild >= cardPrefab._cardInfo.cost)
                     { 
                         _dropCard.DropNewCardInPanel(cardPrefab, zoneTag);
                         _hendlerCardsInTableFromHand.enabled = false;
                         transform.SetParent(_dropCard.rangeBuildZone.transform);
                         _hendlerController.CardInTable();
                         if (_abilityActivated != null)
                        _abilityActivated.ActivateAbility();
                    }
                     else
                     {
                         transform.SetParent(_objectToPool.handTransform.transform);
                     } 
                     break;
                 default:
                     transform.SetParent(_objectToPool.handTransform.transform);
                     break;
             }
        }
        else
        {
            transform.SetParent(_objectToPool.handTransform.transform);
        }
    }
}
                                                                                            