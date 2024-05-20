namespace Turn.PhaseSettings.BattelPhase.PlayerPhase
{
    public interface IChangePhase
    {
        void ChangePhase(ref bool endPhase, ref bool beginPhase, string buttonText, string PhaseText);
        void FirstTurn(ref bool beginPhase, string buttonText, string PhaseText);
    }
}