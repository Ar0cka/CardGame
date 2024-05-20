namespace Zenject
{
    public class InjectAssigningAttakers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AssigningAttackers>().AsSingle();
        }
    }
}