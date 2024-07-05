using CardSettings.Tokens.EffectOnFriendlyCards;

namespace Zenject
{
    public class InjectTokenCardEffect : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEffectOnFriendlyCard>().To<TokenEffectOnFriendlyCards>().AsTransient().NonLazy();
        }
    }
}