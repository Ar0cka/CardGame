using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TurnController : MonoBehaviour, ITurn
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnUI;
    [SerializeField] private EnemyBattlePhase enemyBattlePhase;
    [SerializeField] private DeckController _deckController;
    
    [SerializeField] private GameObject _deadMenu;
    [SerializeField] private PlayerFase _phaseController;
    [SerializeField] private EnemyPhase _enemyPhaseController;
    [SerializeField] private ManaManager _manaManager;
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

    public void InitializeTurnConttoller() // процесс первоначальной инициализации при загрузке сцены.
    {
        endTurnButton.onClick.AddListener(OnClickButtonEndTurn);

        _player = FindObjectOfType<PlayerBattleScene>();

        turnUI.text = _turn.ToString();
    }

    public void BeginTurnPlayer()
    {
        _isTurnPlayer = true;
        if (_player.currentHp > 0)
        {
            _phaseController.BeginBuildPhase();
            _deckController.TakeCardFromDeck();
            
            #region ChangeCountTurn

            _turn++;
            turnUI.text = _turn.ToString();

            #endregion
            
            _manaManager.AddManaToPool();
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
        else if (_phaseController._isAttackPhase)
        {
            _phaseController.EndPhase();
        }
        
    } // начало фазы перестановки
    
    public void TurnEnemy()
    {
       _enemyPhase.IsAssingDefense();
    } // ход противника

    private IEnumerator DelayStarTurnPlayer()
    {
        endTurnButton.interactable = false;
        yield return new WaitForSeconds(3);
        BeginTurnPlayer();
        endTurnButton.interactable = true;
    } // задержка перед началом хода игрока
    
    private void OnClickButtonEndTurn()
    {
        if (_phaseController != null && _deckController != null)
        {
            TurnControllerPlayer();
        }
    }
}
