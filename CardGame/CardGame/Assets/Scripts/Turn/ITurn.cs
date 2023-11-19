
public interface ITurn 
{
  bool isTurnPlayer { get; }
  bool isTurnEnemy { get; }
  int turn { get;}

    void TurnPlayer();
    void TurnEnemy();
}
