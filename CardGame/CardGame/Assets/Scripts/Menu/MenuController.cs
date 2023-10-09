using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject panelNewGame;
    [SerializeField] private GameObject panelLoadGame;
    public void NewGameClick()
    {
        panelNewGame.SetActive(true);
    }
    public void LoadGameClick()
    {
        panelLoadGame.SetActive(true);
    }
}
