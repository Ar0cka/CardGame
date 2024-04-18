using System;
using System.Collections.Generic;
using UnityEngine;


public class DeathToken : MonoBehaviour
{
    public string nameToken { get; set; }

    [SerializeField] private int damageForOneToken;
    public int _damageForOneToken => damageForOneToken;
}
