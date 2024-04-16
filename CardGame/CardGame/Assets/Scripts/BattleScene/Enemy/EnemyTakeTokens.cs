
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeTokens
{
    private Dictionary<string, CardPrefab> tokenInEnemy = new Dictionary<string, CardPrefab>();

    public void AddNewToken(string nameToken, CardPrefab cardWithTokenSystem)
    {
        tokenInEnemy.Add(nameToken, cardWithTokenSystem);
    }

    public void DealDamageFromToken(EnemyBattlePhase _enemyBattlePhase)
    {
        List<int> damageFromToken = new List<int>();

        foreach (var token in tokenInEnemy)
        {
            var card = token.Value;
            var deathToken = card.GetComponent<DeathToken>();
            
            damageFromToken.Add(deathToken._damageForOneToken);
        }
        
        //Вызов метода который проведет Damage по nemy 
        
        //Удаление первого токена который вошел в словарь, удаление первого токена который зашел в лист
    }
}
