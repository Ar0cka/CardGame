using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    #region Serialize

    [SerializeField] private Image _iconCard;
    [HideInInspector] public string currentZoneTag;
    [SerializeField] private CardUI _cardUI;

    [SerializeField] private bool haveDeathToken = false;
    public bool _haveDeathToken => haveDeathToken;
    
    [SerializeField] private CardInfo cardInfo;
    private CardZoneController _removeCardZoneController;
    public CardInfo _cardInfo => cardInfo;
    
    public string uniqueID;
    
    private int currentHitPoint;
    public int _currentHitPoint => currentHitPoint;
    
  
    private EnemyBattlePhase _enemyBattlePhase;
    public bool isBattleZone = false;

    #endregion
    
    private void Awake()
    {
        _removeCardZoneController = FindObjectOfType<CardZoneController>();
        
        if (cardInfo != null)
        _iconCard.sprite = cardInfo.iconCard;
        
        uniqueID = Guid.NewGuid().ToString();

        _enemyBattlePhase = FindObjectOfType<EnemyBattlePhase>();

        currentHitPoint = cardInfo.hitPoint;
        
        _cardUI.UpdateHpAndDamageUI();
    }

    public void SetZoneTag(string zone)
    {
        currentZoneTag = zone;
    }

    public void DealDamage(GameObject target)
    {
        _enemyBattlePhase.AttackEnemy(_cardInfo.damage);
    }

    public void TakeDamage(ref int summaAttack)
    {
        int hpDefenser = currentHitPoint;
        currentHitPoint -= summaAttack;
        summaAttack -= hpDefenser;

        if (currentHitPoint > 0)
        {
            _cardUI.UpdateHpAndDamageUI(); 
        }
        else if (currentHitPoint < 0)
        {
            _removeCardZoneController.RemoveCardFromBattleZone(uniqueID);
        }
    }
}
