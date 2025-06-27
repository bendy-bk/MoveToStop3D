
public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {

    }

    public void OnExcute(Bot t)
    {
        //if (t.CharacterCount > 0)
        //{
        //    t.IsAttacking = true;
        //    t.Attack();
        //}
        //else
        //{
        //    t.ChangeState(new PatrolState());
        //}
    }

    public void OnExit(Bot t)
    {

    }
}

