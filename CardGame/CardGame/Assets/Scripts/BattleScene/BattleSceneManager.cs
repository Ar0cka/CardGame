using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField] private DeckController _deckController;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private EnemyAndPlayerUI _enemyAndPlayerUI;
    [SerializeField] private TurnController _turnController;
    [SerializeField] private ZoneDropBegin _zoneDropBegin;
    [SerializeField] private ManaManager _manager;
    private void Awake()
    {
        _zoneDropBegin.InitializeZoneDrop();
        _enemyController.InitializeEnemyController();
        _turnController.InitializeTurnConttoller();
        _manager.InitializeManaManager();
        _deckController.Initialize();
        _enemyAndPlayerUI.InitializeUI();
    }
}
