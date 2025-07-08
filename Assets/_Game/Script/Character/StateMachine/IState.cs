public interface IState<T>
{
    void OnEnter(T t);
    void OnExecute(T t);
    void OnExit(T t);
}

// StateMachine controller
public class StateMachine<T>
{
    private IState<T> currentState;

    public void ChangeState(T owner, IState<T> newState)
    {
        currentState?.OnExit(owner);
        currentState = newState;
        currentState?.OnEnter(owner);
    }

    public void Update(T owner)
    {
        currentState?.OnExecute(owner);
    }
}