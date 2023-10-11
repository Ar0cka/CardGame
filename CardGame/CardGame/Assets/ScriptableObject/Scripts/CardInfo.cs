using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "New Card")]
public class CardInfo : ScriptableObject
{
    public enum TypeCard{
        Attack, 
        Block,
        Regen,
    }
    [SerializeField] private Sprite _iconCard;
    [SerializeField] private TypeCard _type;
    [SerializeField] private string _nameCard;
    [SerializeField] private string _descriptionCard;
    [SerializeField] private int _damage;
    [SerializeField] private int _cost;

    public Sprite iconCard => _iconCard;
    public TypeCard type => _type;
    public string nameCard => _nameCard;
    public string descriptionCard => _descriptionCard;
    public int damage => _damage;
    public int cost => _cost;
}
