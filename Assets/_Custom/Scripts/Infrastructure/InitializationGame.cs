
using UnityEngine;
using Zenject;

public class InitializationGame : MonoBehaviour
{
    private IGameStateMachine _gameStateMachine;

    [Inject]
    void Construct(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    private void Start()
    {
        _gameStateMachine.Enter<LoadSceneState, string>(InfrastructureAssetPath.MainMenuScene);
        DontDestroyOnLoad(this);
    }

    public class Factory : PlaceholderFactory<InitializationGame>
    {
    }
}
