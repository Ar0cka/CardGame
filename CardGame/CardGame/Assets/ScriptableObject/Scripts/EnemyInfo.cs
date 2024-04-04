using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "new enemy")]
    public class EnemyInfo : ScriptableObject {
    [SerializeField] private string _nameEnemy;
    [SerializeField] private int _damage, _hp, _defense, _buff;
    [SerializeField] private Sprite _monsterSprite;

    public string nameEnemy => _nameEnemy;
    public int damage => _damage;
    public int hp => _hp;
    public int defense => _defense;
    public int buff => _buff;
    public Sprite monsterSprite => _monsterSprite;

    public void LoadSetting(string monsterName, int damage, int hp, int defense, int buff, Sprite monsterSprite)
    {
        _nameEnemy = monsterName;
        _damage = damage;
        _hp = hp;
        _defense = defense;
        _buff = buff;
        _monsterSprite = monsterSprite;
    }
    
    }
