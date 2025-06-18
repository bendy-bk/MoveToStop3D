
public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constants.ANIM_ATTACK);
    }

    public void OnExcute(Bot t)
    {
        if(t.CharacterCount > 0)
        {
            t.isAttack = true;
            t.Attack();
        } else
        {
            t.ChangeState(new PatrolState());
        } 
    }

    public void OnExit(Bot t)
    {
        
    }
}

