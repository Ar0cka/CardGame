using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildRes", menuName = "new build")]
public class ResursBuild : ScriptableObject
{
    public enum TypeBuild
    {
        HumanMana, 
        BuildMana,
    }
    
    [SerializeField] private string name;
    [SerializeField] private int amountGiveMana;
    [SerializeField] private Sprite _iconCard;
    [SerializeField] private TypeBuild _type;

    public string _nameBuild => name;
    public int _amountMana => amountGiveMana;
    public Sprite iconCard => _iconCard;
    public TypeBuild typeBuild => _type;
}
