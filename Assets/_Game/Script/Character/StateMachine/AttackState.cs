
using UnityEngine.UIElements;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.Attack();
    }

    public void OnExecute(Bot t)
    {
        if (!t.IsAttacking)
        {
            t.ChangeState(new DetectState());
        }
    }

    public void OnExit(Bot t){}
}

