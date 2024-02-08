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
    [SerializeField] private Sprite _iconCard;
    [SerializeField] private SubtypeCard _subtype;
    [SerializeField] private CardsType _cardsType;
    [SerializeField] private string _nameCard;
    [SerializeField] private string _descriptionCard;
    [SerializeField] private int _cost;
    private int _hitPoint; 
    private int _damageAttack, _defense;
    private int _heal;
   
    #endregion

    public Sprite iconCard => _iconCard;
    public SubtypeCard subtype => _subtype;
    public CardsType cardType => _cardsType;
    public string nameCard => _nameCard;
    public string descriptionCard => _descriptionCard;
    public int cost => _cost;
    
    #region SettersParametrs
    
    public int damage
    {
        get { return _damageAttack; }
        set { _damageAttack = value; }
    }

    public int defense
    {
        get { return _defense; }
        set { _defense = value; }
    }

    public int heal
    {
        get { return _heal;}
        set { _heal = value; }
    }
    #endregion
}
