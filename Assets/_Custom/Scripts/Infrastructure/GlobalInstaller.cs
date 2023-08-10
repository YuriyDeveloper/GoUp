
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private InitializationGame _initializationGame;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    public override void InstallBindings()
    {

        BindInitializationGame();
        BindCoroutineRunner();
        BindGameStateMachine();
        BindSceneLoader();
    } 
    
  
    
    private void BindInitializationGame()
    {
        Container
           .BindFactory<InitializationGame, InitializationGame.Factory>()
           .FromInstance(_initializationGame);
    }
    
    private void BindCoroutineRunner()
    {
        Container
            .Bind<ICoroutineRunner>()
            .To<CoroutineRunner>()
            .FromInstance(_coroutineRunner)
            .AsSingle();

    }
    private void BindGameStateMachine()
    {
        Container
            .Bind<IGameStateMachine>()
            .FromSubContainerResolve()
            .ByInstaller<GameStateMachineInstaller>()
            .AsSingle();
    }

    private void BindSceneLoader()
    {
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
    }

}
