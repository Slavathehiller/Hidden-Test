using Assets.Scripts;
using Assets.Scripts.Factories;
using Zenject;

public class PlayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SceneAssetFactory>().AsTransient();
        Container.BindInterfacesTo<UIAssetFactory>().AsTransient();            
    }
}

