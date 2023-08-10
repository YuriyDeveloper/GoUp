using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuButtonIventsCoordinator : MonoBehaviour
{
    private IGameStateMachine _gameStateMachine;

    [Inject]
    void Construct(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void LoadGameLevelScene()
    {
        _gameStateMachine.Enter<LoadSceneState, string>(InfrastructureAssetPath.GameLevelScene);
    }
}