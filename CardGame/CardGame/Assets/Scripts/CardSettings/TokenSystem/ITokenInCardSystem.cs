namespace CardSettings.CardPrefabSettings
{
    public interface ITokenInCardSystem
    {
        bool canAttack { get; }
        
        void CardHaveFearToken();
        void DeleteFearToken();
    }
}