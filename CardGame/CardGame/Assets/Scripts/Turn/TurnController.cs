using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turn.PhaseSettings.BattelPhase.PlayerPhase;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class TurnController : MonoBehaviour, ITurn
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnUI;
    [SerializeField] private DeckController _deckController;
    
    [SerializeField] private GameObject _deadMenu;
    [SerializeField] private PlayerPhase _phaseController; 
    [SerializeField] private ManaController manaController;
    [SerializeField] private EnemyPhase _enemyPhase;
    
    private PlayerBattleScene _player;

    [Inject] private PlayerPhaseSettings _playerPhaseSettings;

    #region parametrs
    
    private bool _isTurnPlayer = false;
    private bool _isTurnEnemy = false;

    private int _turn = 1;
    #endregion

    #region ITurnParametrs
    public bool isTurnPlayer => _isTurnPlayer;
    public bool isTurnEnemy => _isTurnEnemy;

    public int turn => turn;
    #endregion

    private void Awake()
    {
        _player = FindObjectOfType<PlayerBattleScene>();
    }

    public void InitializeTurnConttoller() // процесс первоначальной инициализации при загрузке сцены.
    {
        endTurnButton.onClick.AddListener(OnClickButtonEndTurn);
        BeginTurnPlayer();
        turnUI.text = _turn.ToString();
    }

    public void BeginTurnPlayer()
    {
        SwapTurn();
        if (_player.currentHp > 0)
        {
            _phaseController.TurnControllerPlayer();
            _deckController.TakeCardFromDeck();
            
            #region ChangeCountTurn

            _turn++;
            turnUI.text = _turn.ToString();

            #endregion
            
            manaController.AddManaToPool();
        }
        else
        {
            _deadMenu.SetActive(true);
        }
    }
    
    public void TurnEnemy()
    {
        _deckController.DiscardCardFromHand();
        SwapTurn();
        _enemyPhase.EnemyPreparationPhase();
    } 
    
    private void OnClickButtonEndTurn()
    {
        if (_phaseController != null && _deckController != null)
        {
            _phaseController.TurnControllerPlayer();
        }
    }

    public void SwapTurn() 
    {
        if (_isTurnPlayer)
        {
            _isTurnEnemy = true;
            _isTurnPlayer = false;
            endTurnButton.interactable = false;
        }
        else
        {
            _isTurnEnemy = false;
            _isTurnPlayer = true;
            endTurnButton.interactable = true;
        }
    }
    
}
