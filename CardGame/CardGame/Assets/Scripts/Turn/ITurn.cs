
public interface ITurn 
{
  bool isTurnPlayer { get; }
  bool isTurnEnemy { get; }
  int turn { get;}

    void TurnPlayerBegin();
    void TurnPlayerPenutationPhase();
    void TurnPlayerBattlePhase();
    void TurnPlayerEnd();
    void TurnEnemy();
}
