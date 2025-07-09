using UnityEngine.UIElements;

public class DetectState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        
        t.FindTarget();

        if (t.TargetCharacter != null)
        {
            t.IsMoving = true;
            t.ChangeState(new MoveState());
        }
        else
        {
            t.ChangeState(new IdleState());
        }
    }

    public void OnExecute(Bot t){}

    public void OnExit(Bot t){}

    
}

