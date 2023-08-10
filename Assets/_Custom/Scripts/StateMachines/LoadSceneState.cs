
using UnityEngine.SceneManagement;
using Zenject;

public class LoadSceneState : IPaylodedState<string>
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ISceneLoader _sceneLoader;

    public LoadSceneState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter(string sceneName)
    {
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {

    }

    private void OnLoaded()
    {
        if (SceneManager.GetActiveScene().name == InfrastructureAssetPath.MainMenuScene)
        {
            _gameStateMachine.Enter<GameLoopState>();
        }
    }

    public class Factory : PlaceholderFactory<IGameStateMachine, LoadSceneState>
    {
    }
}
