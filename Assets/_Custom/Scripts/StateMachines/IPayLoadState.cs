
public interface IPaylodedState<TPayload> : IExitableState
{
    void Enter(TPayload payload);
}