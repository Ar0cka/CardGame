using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySettings : MonoBehaviour
{
    #region EnemyRegion
    
    private List<EnemyInfo> enemyInfo;
    private SpriteRenderer _spriteRenderer;
    private EnemyTransit _monstersTransit;
    private BoxCollider2D _boxColliderTriger;
    private BoxCollider2D _boxCollider2D;
    
    #endregion
    
    #region MonstersParametrs
    
    private string _name;
    private int _damage, _maxHitPoints,
        _defense;

    private Sprite _monsterSprite;
    
    public string uniqueID; 


    public string name => _name;
    public int damage => _damage;
    public int maxHitPoints => _maxHitPoints;
    public int defense => _defense;
    public int _currentHitPointsEnemy;
   
    #endregion
    
    public void InitializeEnemyController()
    {
        #region SerilizeComponent
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxColliderTriger = GetComponent<BoxCollider2D>();
        _boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        _monstersTransit = FindObjectOfType<EnemyTransit>();
        
        #endregion

        #region LoadMonster

        enemyInfo = _monstersTransit.LoadMonsterData();
        for (int i = 0; i < enemyInfo.Count; i++)
        {
            _name = enemyInfo[i].nameEnemy;
            _maxHitPoints = enemyInfo[i].hp;
            _damage = enemyInfo[i].damage;
            _defense = enemyInfo[i].defense;
            _monsterSprite = enemyInfo[i].monsterSprite;
        }
        LoadSpriteMonster();

        #endregion

        _currentHitPointsEnemy = _maxHitPoints;

        #region InitializeUniqueID;

        uniqueID = Guid.NewGuid().ToString();

        #endregion
        
        SetSettingsCollider();
    }

    #region Settings

    private void SetSettingsCollider()
    {
        if (_spriteRenderer != null)
        {
            _boxColliderTriger.size = _spriteRenderer.size;
            _boxCollider2D.size = _spriteRenderer.size;
        }
    }
    
    private void LoadSpriteMonster()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = _monsterSprite;
    }

    #endregion
}
