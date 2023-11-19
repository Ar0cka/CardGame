using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour, ITurn
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnUI;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private DeckController _deckController;

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

    public void InitializeTurnConttoller()
    {
        endTurnButton.onClick.AddListener(OnClickButtonEndTurn);

        turnUI.text = _turn.ToString();

        _isTurnPlayer = true;
        _isTurnEnemy = false;
    }
    private void Update()
    {
        if (_isTurnEnemy) 
        {
            TurnEnemy();
        }
    }
    public void TurnPlayer()
    {
        _deckController.TakeCardFromDeck();
        _turn++;
        turnUI.text = _turn.ToString();
    }
 
    public void TurnEnemy()
    {
        _enemyController.AttackPlayer();
        #region ChangeTurn
        _isTurnEnemy = false;
        _isTurnPlayer = true;
        #endregion

        StartCoroutine(DelayStarTurnPlayer());
    }

    private IEnumerator DelayStarTurnPlayer()
    {
        endTurnButton.interactable = false;
        yield return new WaitForSeconds(3);
        TurnPlayer();
        endTurnButton.interactable = true;
    }
    private void OnClickButtonEndTurn()
    {
        _isTurnPlayer = false;
        _isTurnEnemy = true;
        _deckController.DiscardCardFromHandToDiscardDeck();
    }
}
