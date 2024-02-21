using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Card", menuName = "New Card")]
public class CardInfo : ScriptableObject
{
    public enum SubtypeCard
    {
        DefenseBuild,
        AttackHuman,
        AttackRangeHuman,
        AttackRangeBuild,
        AuxiliaryBuild,
    }

    public enum CardsType
    {
        Human,
        Build
    }

    #region initialize parametrs
    [SerializeField] protected Sprite _iconCard;
    [SerializeField] protected SubtypeCard _subtype;
    [SerializeField] protected CardsType _cardsType;
    [SerializeField] protected string _nameCard;
    [SerializeField] protected string _descriptionCard;
    [SerializeField] protected int _cost;
    [SerializeField] protected int _damageAttack;
    [SerializeField] protected int _hitPoint;
   
    public Sprite iconCard => _iconCard;
    public SubtypeCard subtype => _subtype;
    public CardsType cardType => _cardsType;
    public string nameCard => _nameCard;
    public string descriptionCard => _descriptionCard;
    public int cost => _cost;
    public int damage => _damageAttack;
    public int hitPoint => _hitPoint;
    #endregion
}


