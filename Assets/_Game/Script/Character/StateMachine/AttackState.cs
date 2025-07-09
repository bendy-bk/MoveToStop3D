
using UnityEngine.UIElements;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        if (t.CharacterCount > 0) {
            t.Attack();
        } else
        {
            t.ChangeState(new DetectState());
        }
    }

    public void OnExecute(Bot t)
    {
        
    }

    public void OnExit(Bot t){}
}

