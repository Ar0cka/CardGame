using System.Security.Principal;
using CardSettings.Tokens;

namespace Zenject
{
    public class InjectTokenEffectOnOpponent : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITokenEffectOnOpponent>().To<TokenEffectOnOpponent>().AsSingle();
        }
    }
}