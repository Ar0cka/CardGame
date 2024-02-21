using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Auxiliary Build", menuName = "New Auxiliary Build")]
public class AuxiliaryBuild : CardInfo
{
    public AuxiliaryBuild()
    { 
        _subtype = SubtypeCard.AuxiliaryBuild;
    }
    
    [SerializeField] private int _heal;
    
    public int heal => _heal;
}

