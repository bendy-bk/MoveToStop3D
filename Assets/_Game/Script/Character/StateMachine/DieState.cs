public class DieState : IState<Bot>
{
    
    public void OnEnter(Bot t)
    {
        t.StopMove();
        t.OnDeath();
    }

    public void OnExecute(Bot t){}

    public void OnExit(Bot t){}

    
}

