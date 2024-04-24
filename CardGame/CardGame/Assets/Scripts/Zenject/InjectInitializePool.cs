using Zenject;

public class InjectInitializePool : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InitializeObjectToPool>().AsSingle();
    }
}
