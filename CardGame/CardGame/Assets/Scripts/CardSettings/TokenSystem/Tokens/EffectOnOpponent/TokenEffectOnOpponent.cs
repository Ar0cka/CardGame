
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TokenEffectOnOpponent
{
    private static TokenEffectOnOpponent _instance;
    public static TokenEffectOnOpponent Instance => _instance ?? (_instance = new TokenEffectOnOpponent());
    
    private Dictionary<CardPrefab, EnemyBattlePhase> deathTokenInEnemy = new Dictionary<CardPrefab, EnemyBattlePhase >();
    public Dictionary<CardPrefab, EnemyBattlePhase> _deathTokenInEnemy => deathTokenInEnemy;

    private EnemyBattlePhase _enemy;
    
    private TokenEffectOnOpponent()
    {
        
    }

    public void AddNewTokenEnemy(EnemyBattlePhase enemyBattlePhase, CardPrefab cardWithTokenSystem) // После атаки будет добавляться токен на определенный таргет
    {
        if (cardWithTokenSystem._haveDeathToken)
        {
            if (!deathTokenInEnemy.ContainsKey(cardWithTokenSystem))
            {
                deathTokenInEnemy.Add(cardWithTokenSystem, enemyBattlePhase);
                Debug.Log(deathTokenInEnemy.Count);
            }
        }
    }
    
    public void DealDamageFromDeathToken() // Метод будет срабатывать в конце хода игрока 
    {
        if (deathTokenInEnemy.Count > 0)
        {
            List<int> damageFromToken = new List<int>();

            foreach (var token in deathTokenInEnemy)
            {
                var card = token.Key;
                var deathToken = card.GetComponent<DeathToken>();
                
                var enemy = token.Value; 
                _enemy = enemy.GetComponent<EnemyBattlePhase>();
                
                damageFromToken.Add(deathToken._damageForOneToken);
            }

            _enemy.TakeDamageFromDeathTokens(damageFromToken);
            Debug.Log("Damage from tokens");
            
            KeyValuePair<CardPrefab, EnemyBattlePhase> firstPosition = deathTokenInEnemy.FirstOrDefault();

            deathTokenInEnemy.Remove(firstPosition.Key);
            
            if (damageFromToken.Count > 0)
            {
                    damageFromToken.RemoveAt(0);
            }
        } 
    }
}

