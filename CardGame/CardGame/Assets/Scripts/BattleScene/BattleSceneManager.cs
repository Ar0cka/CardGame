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
    private void Awake()
    {
        _enemyController.InitializeEnemyController();
        _turnController.InitializeTurnConttoller();
        _deckController.Initialize();
        _enemyAndPlayerUI.InitializeUI();
    }
}
