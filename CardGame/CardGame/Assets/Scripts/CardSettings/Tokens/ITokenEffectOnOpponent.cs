using System.Collections.Generic;

namespace CardSettings.Tokens
{
    public interface ITokenEffectOnOpponent
    {
        Dictionary<CardPrefab, EnemyBattlePhase> _deathTokenInEnemy { get;}
        
        void AddNewTokenEnemy(EnemyBattlePhase enemyBattlePhase, CardPrefab cardWithTokenSystem);

        void DealDamageFromDeathToken();
    }
}