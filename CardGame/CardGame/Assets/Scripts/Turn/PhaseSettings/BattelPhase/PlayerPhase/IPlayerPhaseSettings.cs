using System.Collections.Generic;

namespace Turn.PhaseSettings.BattelPhase.PlayerPhase
{
    public interface IPlayerPhaseSettings
    {
        bool _isBuildPhase { get; }
        bool _isPenutationPhase { get; }
        bool _isBattlePhase { get; }
        bool _isEndPhase { get; }
        bool _isAttackPhase { get; }

        void BeginBuildPhase(List<CardPrefab> cardsHand, List<CardPrefab> cardsZone, ChangePhaseController changePhase);

        void BeginPenutationPhase(IBeginAttackUnits beginAttackUnits, IRepackLists repackLists,
            CardZoneController _zoneCards);

        void BeginBattle(List<CardPrefab> cardsZone);
        void BeginAttackPhase(List<CardPrefab> cardsZone);
        void EndPhase();
        void BeginEnemyTurn();
    }
}