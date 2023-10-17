using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "new enemy")]
public class EnemyInfo : ScriptableObject {
    [SerializeField] private string nameEnemy;
    [SerializeField] private int damage, hp, defense;
    [SerializeField] private Sprite monsterSprite;
    
    public Sprite _monsterSprite => monsterSprite;
    public string _name => nameEnemy;
    public int _defense => defense;
    public int _damage => damage;
    public int _hp => hp;
}
