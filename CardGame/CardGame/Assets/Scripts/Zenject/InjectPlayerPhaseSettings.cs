using Turn.PhaseSettings.BattelPhase.PlayerPhase;

namespace Zenject
{
    public class InjectPlayerPhaseSettings: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerPhaseSettings>().To<PlayerPhaseSettings>().AsSingle();
        }
    }
}