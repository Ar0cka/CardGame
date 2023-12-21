using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour, ITurn
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnUI;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private DeckController _deckController;
    private PlayerBattleScene _player;
    [SerializeField] private GameObject _deadMenu;

    [SerializeField] private Fase _phaseController;

    #region parametrs
    private bool _isTurnPlayer;
    private bool _isTurnEnemy;

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

        _isTurnPlayer = true;
        _isTurnEnemy = false;
    }
    private void FixedUpdate()
    {
        if (_isTurnEnemy) 
        {
            TurnEnemy();
        }
    } // проверка на ход противника
    public void TurnPlayerBegin()
    {
        _phaseController.BeginBuildPhase();
        if (_deckController != null)
        {
            _deckController.TakeCardFromDeck();
        }
        #region ChangeCountTurn
        _turn++;
        turnUI.text = _turn.ToString();
        #endregion
    } // начало фазы строение игрока

    public void TurnPlayerPenutationPhase()
    {
        _phaseController.BeginPenutationPhase();
    } // начало фазы перестановки

    public void TurnPlayerBattlePhase()
    {
        _phaseController.BeginBattle();
    } // начало боевой фазы

    public void TurnPlayerEnd()
    {
        _phaseController.EndPhase();
    } // начало заключительной фазы
    
    public void TurnEnemy()
    {
        if (_player.currentHp > 0)
        {
            _enemyController.AttackPlayer();
            #region ChangeTurn
            _isTurnEnemy = false;
            _isTurnPlayer = true;
            #endregion
            StartCoroutine(DelayStarTurnPlayer());
        }
        else
        {
            _deadMenu.SetActive(true);
        }
    } // ход противника

    private IEnumerator DelayStarTurnPlayer()
    {
        endTurnButton.interactable = false;
        yield return new WaitForSeconds(3);
        TurnPlayerBegin();
        endTurnButton.interactable = true;
    } // задержка перед началом хода игрока
    private void OnClickButtonEndTurn()
    {
        if (_phaseController != null && _deckController != null)
        {
            if (_phaseController._isBuildPhase)
            {
                TurnPlayerPenutationPhase();
            }
            else if (_phaseController._isPenutationPhase)
            {
                TurnPlayerBattlePhase();
            }
            else if (_phaseController._isBattlePhase)
            {
                TurnPlayerEnd();
            }
            else if (_phaseController._isEndPhase)
            {
                _isTurnPlayer = false;
                _isTurnEnemy = true;
                _deckController.DiscardCardFromHandToDiscardDeck();
            }
        }
    } // событие отвечающее за окончания фаз
}
