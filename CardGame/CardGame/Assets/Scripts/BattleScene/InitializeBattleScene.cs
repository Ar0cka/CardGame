using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InitializeBattleScene : MonoBehaviour
{
    [SerializeField] private DeckController _deckController;
    [FormerlySerializedAs("_enemyController")] [SerializeField] private EnemySettings enemySettings;
    [SerializeField] private EnemyAndPlayerUI _enemyAndPlayerUI;
    [SerializeField] private TurnController _turnController;
    [SerializeField] private ZoneDropBegin _zoneDropBegin;
    [FormerlySerializedAs("_manager")] [SerializeField] private ManaController controller;
    
    private void Awake()
    {
        _zoneDropBegin.InitializeZoneDrop();
        enemySettings.InitializeEnemyController();
        _turnController.InitializeTurnConttoller();
        controller.InitializeManaManager();
        _deckController.Initialize();
        _enemyAndPlayerUI.InitializeUI();
        
    }
}
