namespace CardSettings.Tokens.EffectOnFriendlyCards
{
    public interface IEffectOnFriendlyCard
    {
        void AddNewTokenInFriendCard(CardPrefab tokenAffectedCard);
        void ActionFearToken();
    }
}