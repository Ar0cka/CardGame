using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginBattleFase : MonoBehaviour
{
    [SerializeField] private string nameScene; 
    public void BeginBattle()
    {
        SceneManager.LoadScene(nameScene);
    }
}
