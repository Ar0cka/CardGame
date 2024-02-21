using UnityEngine;


[CreateAssetMenu(fileName = "BuildRes", menuName = "new build")]
public class ResursBuild : ScriptableObject
{
    public enum TypeBuild
    {
        HumanMana, 
        BuildMana,
    }
    
    [SerializeField] private string nameResursesBuildCard;
    [SerializeField] private int amountGiveMana;
    [SerializeField] private Sprite _iconCard;
    [SerializeField] private TypeBuild _type;

    public string NameResursesBuildCardBuild => nameResursesBuildCard;
    public int _amountMana => amountGiveMana;
    public Sprite iconCard => _iconCard;
    public TypeBuild typeBuild => _type;
}
