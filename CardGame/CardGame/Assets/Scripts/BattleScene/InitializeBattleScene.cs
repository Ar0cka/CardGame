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
    [FormerlySerializedAs("controller")] [SerializeField] private ManaController manaController;
    
    private void Awake()
    {
        manaController.InitializeManaManager();
        _deckController.Initialize();
        _zoneDropBegin.InitializeZoneDrop();
        enemySettings.InitializeEnemyController();
        _enemyAndPlayerUI.InitializeUI();
        _turnController.InitializeTurnConttoller();
    }
}
