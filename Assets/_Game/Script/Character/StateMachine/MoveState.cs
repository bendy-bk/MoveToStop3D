using UnityEngine.UIElements;

public class MoveState : IState<Bot>
{

    public void OnEnter(Bot t)
    {
        t.StartMove();
        t.ChangeAnim(Constants.ANIM_RUN);
    }

    public void OnExecute(Bot t)
    {
        if (t.TargetCharacter == null)
        {
            t.ChangeState(new DetectState());
            return;
        }

        t.MoveToTarget();

        if (t.IsTargetInAttackRange())
        {
            t.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot t)
    {
        t.StopMove();
    }

    
}

