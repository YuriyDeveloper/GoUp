
using Zenject;

public class GameLoopState : IState
{
    private readonly IGameStateMachine _gameStateMachine;

    private GameLoopState(
        IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        UnityEngine.Debug.Log("GameLoop state start");
    }

    public void Exit()
    {
        UnityEngine.Debug.Log("GameLoop state ended");
    }


    public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
    {
    }
}
