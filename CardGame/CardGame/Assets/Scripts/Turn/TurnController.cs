using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour, ITurn
{
    [SerializeField] private Button endTurnButton;

    #region parametrs
    private bool _isTurnPlayer;
    private bool _isTurnEnemy;

    private int _turn;
    #endregion

    #region ITurnParametrs
    public bool isTurnPlayer => _isTurnPlayer;
    public bool isTurnEnemy => _isTurnEnemy;

    public int turn => turn;
    #endregion

    private void Awake()
    {
        _isTurnPlayer = true;
        _isTurnEnemy = false;
    }
    private void Update()
    {
        if (_isTurnPlayer && !_isTurnEnemy)
        {
            TurnPlayer();
        }
        else if (_isTurnEnemy && !_isTurnPlayer) 
        {
            TurnEnemy();
        }
    }
    public void TurnPlayer()
    {
        _turn++;
    }

    public void TurnEnemy(){
        
    }

    private void OnClickButtonEndTurn()
    {
        _isTurnPlayer = false;
        _isTurnEnemy = true;
    }
}
