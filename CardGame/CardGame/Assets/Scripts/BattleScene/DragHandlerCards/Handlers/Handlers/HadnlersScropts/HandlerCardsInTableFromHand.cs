using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.XR;


public class HandlerCardsInTableFromHand : AbstractHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform handTransform;
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
        handTransform = FindObjectOfType<HandCards>().GetComponent<RectTransform>();
        
        Card = FindObjectOfType<CardZoneController>();
        _player = FindObjectOfType<PlayerBattleScene>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        isBeginDrag = true;
        transform.SetParent(Card._hendlerZone.transform);
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
                         Card.DropNewCardInPanel( cardPrefab, zoneTag);
                         _hendlerCardsInTableFromHand.enabled = false;
                         
                         transform.SetParent(Card.miliArmyZone.transform);
                         _handlerController.CardInTable();
                         
                         if (_abilityActivated != null)
                        _abilityActivated.ActivateAbility();
                     }
                     break;
            
                 case "RangeSolder":
                     if (typeCard == CardInfo.SubtypeCard.AttackRangeHuman && _player.manaHuman >= cardPrefab._cardInfo.cost)
                     {
                         Card.DropNewCardInPanel(cardPrefab, zoneTag);
                         _hendlerCardsInTableFromHand.enabled = false;
                         transform.SetParent(Card.rangeArmyZone.transform);
                         _handlerController.CardInTable();
                         if (_abilityActivated != null)
                        _abilityActivated.ActivateAbility();
                    }
                     break;
            
                 case "RangeBuild":
                     if (typeCard == CardInfo.SubtypeCard.AttackRangeBuild || typeCard == CardInfo.SubtypeCard.AuxiliaryBuild && _player.manaBuild >= cardPrefab._cardInfo.cost)
                     { 
                         Card.DropNewCardInPanel(cardPrefab, zoneTag);
                         _hendlerCardsInTableFromHand.enabled = false;
                         transform.SetParent(Card.rangeBuildZone.transform);
                         _handlerController.CardInTable();
                         if (_abilityActivated != null)
                        _abilityActivated.ActivateAbility();
                    }
                     break;
                 default:
                     ReturnCardInHand();
                     break;
             }
        }
        else
        {
            ReturnCardInHand();
        }

        isBeginDrag = false;
    }

    private void ReturnCardInHand()
    {
        transform.SetParent(handTransform.transform);
    }
}
                                                                                            