using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "new enemy")]
    public class EnemyInfo : ScriptableObject {
    public string nameEnemy;
    public int damage, hp, defense;
    public Sprite monsterSprite;
}
