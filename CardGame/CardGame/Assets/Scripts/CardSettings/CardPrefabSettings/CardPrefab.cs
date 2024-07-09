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
    [SerializeField] private CardUI _cardUI;
    [SerializeField] private CardInfo cardInfo;
    
    private CardZoneController _removeCardZoneController;
    private EnemyBattlePhase _enemyBattlePhase; 
    
    public CardInfo _cardInfo => cardInfo;

    #region tokens

    [SerializeField] private bool haveDeathToken = false;
    public bool _haveDeathToken => haveDeathToken;
    [SerializeField] private bool haveFearToken = false;
    public bool _haveFearToken => haveFearToken;
    [SerializeField] private bool haveLifeToken = false;
    public bool _haveLifeToken => haveLifeToken;

    private bool _isDeletedTokenFromCard = false;
    public bool isDeletedTokenFromCard => _isDeletedTokenFromCard;
    
    #endregion

    #region Heath

    private int currentHitPoint;
    public int _currentHitPoint => currentHitPoint;

    #endregion

    #region BattleActive

    private string _currentZoneTag;
    public string currentZoneTag => _currentZoneTag;
    
    [HideInInspector] public bool isBattleZone = false;

    #endregion
    
    #region CardID

    private string _uniqueID;
    public string uniqueID => _uniqueID;

    #endregion
   
    #endregion 
    
    private void Awake()
    {
        _removeCardZoneController = FindObjectOfType<CardZoneController>();
        
        if (cardInfo != null)
        _iconCard.sprite = cardInfo.iconCard;

        _enemyBattlePhase = FindObjectOfType<EnemyBattlePhase>();

        currentHitPoint = cardInfo.hitPoint;
        
        _cardUI.UpdateHpAndDamageUI();
    }

    #region SetSettings

    public void SetUniqCode()
    {
        _uniqueID = Guid.NewGuid().ToString();
    }
    
    public void SetZoneTag(string zone)
    {
        _currentZoneTag = zone;
    }


    #endregion

    #region ActionDamage

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

    #endregion

    public void DeletedTokenFromCard()
    {
        _isDeletedTokenFromCard = true;
    }

    public void ClearIsDeletedTokenFromCard()
    {
        _isDeletedTokenFromCard = false;
    }
   
}
