using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TurnController : MonoBehaviour, ITurn
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnUI;
    [SerializeField] private DeckController _deckController;
    
    [SerializeField] private GameObject _deadMenu;
    [SerializeField] private PlayerPhase _phaseController;
    [FormerlySerializedAs("_manaManager")] [SerializeField] private ManaController manaController;
    [SerializeField] private EnemyPhase _enemyPhase;
    
    private PlayerBattleScene _player;

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
            _phaseController.BeginBuildPhase();
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
    
    public void TurnControllerPlayer()
    {
        if (_phaseController._isBuildPhase)
        {
            _phaseController.BeginPenutationPhase();
        }
        else if (_phaseController._isPenutationPhase)
        {
            _phaseController.BeginBattle();
        }
        else if (_phaseController._isBattlePhase)
        {
            _phaseController.BeginAttackPhase();
        }
        else if (_phaseController._isEndPhase)
        {
            _phaseController.BeginEnemyTurn();
            TurnEnemy();
        }
        else if (_enemyPhase._isAssignDefense)
        {
           _enemyPhase.AttackEnemyPhase(); 
        }
    } // начало фазы перестановки
    
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
            TurnControllerPlayer();
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
