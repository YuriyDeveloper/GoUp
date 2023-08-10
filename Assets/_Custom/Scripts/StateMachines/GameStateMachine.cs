using System;
using System.Collections.Generic;

public class GameStateMachine : IGameStateMachine
{
    public static Dictionary<Type, IExitableState> _registeredStatesNotResolved;
    private Dictionary<Type, IExitableState> _registeredStates;

    private IExitableState _currentState;

    public GameStateMachine(
        LoadSceneState.Factory loadLevelStateFactory,
        GameLoopState.Factory sandBoxGameModeState)
    {
        _registeredStates = new Dictionary<Type, IExitableState>();
        _registeredStatesNotResolved = new Dictionary<Type, IExitableState>();
        RegisterState(loadLevelStateFactory.Create(this));
        RegisterState(sandBoxGameModeState.Create(this));
    }

    public void Enter<TState>() where TState : class, IState
    {
        TState newState = ChangeState<TState>();
        newState.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
    {

        TState newState = ChangeState<TState>();
        newState.Enter(payload);
    }

    protected void RegisterState<TState>(TState state) where TState : IExitableState
    {
        _registeredStates.Add(typeof(TState), state);
        if (_registeredStates[typeof(TState)] == null)
        {
            _registeredStatesNotResolved.Add(typeof(TState), state);
        }
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {

        _currentState?.Exit();

        TState state = GetState<TState>();

        _currentState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState
    {
        if (_registeredStates[typeof(TState)] != null)
        {
            return _registeredStates[typeof(TState)] as TState;
        }
        else
        {
            _registeredStates[typeof(TState)] = _registeredStatesNotResolved[typeof(TState)];
            return _registeredStates[typeof(TState)] as TState;
        }
    }
}
