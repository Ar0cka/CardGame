namespace CardSettings.CardPrefabSettings
{
    public interface ITokenInCardSystem
    {
        bool _canAttack { get; }
        
        void CardHaveFearToken();
        void DeleteFearToken();
    }
}