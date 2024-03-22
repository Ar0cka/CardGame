using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField] private DeckController _deckController;
    [FormerlySerializedAs("_enemyController")] [SerializeField] private EnemySettings enemySettings;
    [SerializeField] private EnemyAndPlayerUI _enemyAndPlayerUI;
    [SerializeField] private TurnController _turnController;
    [SerializeField] private ZoneDropBegin _zoneDropBegin;
    [SerializeField] private ManaManager _manager;
    private void Awake()
    {
        _zoneDropBegin.InitializeZoneDrop();
        enemySettings.InitializeEnemyController();
        _turnController.InitializeTurnConttoller();
        _manager.InitializeManaManager();
        _deckController.Initialize();
        _enemyAndPlayerUI.InitializeUI();
    }
}
