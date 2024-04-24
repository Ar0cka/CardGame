using Deck.Shuffle;
using Zenject;

public class InjectShuffle : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IShuffle>().To<Shuffle>().AsSingle();
    }
}
