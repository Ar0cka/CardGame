using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "New Card")]
public class CardInfo : ScriptableObject
{
    public enum TypeCard
    {
        DefenseBuild,
        AttackHuman,
        AttackRangeHuman,
        AttackRangeBuild,
        AuxiliaryBuild,
    }
    [SerializeField] private Sprite _iconCard;
    [SerializeField] private TypeCard _type;
    [SerializeField] private string _nameCard;
    [SerializeField] private string _descriptionCard;
    [SerializeField] private int _hitPoint;
    [SerializeField] private int _damageAttack, _damageDefense;
    [SerializeField] private int _cost;

    public Sprite iconCard => _iconCard;
    public TypeCard type => _type;
    public string nameCard => _nameCard;
    public string descriptionCard => _descriptionCard;
    public int hitPoint => _hitPoint;
    public int damageAttack => _damageAttack;
    public int damageDefense => _damageDefense;
    public int cost => _cost;
}
