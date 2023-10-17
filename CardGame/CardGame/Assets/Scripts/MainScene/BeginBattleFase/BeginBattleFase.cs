using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginBattleFase : MonoBehaviour
{
    [SerializeField] private string nameScene;
    private EnemyTransitController _enemyTransitController;

    private void Start()
    {
        _enemyTransitController = GetComponent<EnemyTransitController>();
    }

    public void BeginBattle()
    {
        _enemyTransitController.SaveMonsterData();
        SceneManager.LoadScene(nameScene);
    }
}
