using UnityEngine;

public class IdleState : IState<Bot>
{
    private float timer;
    private float maxTimer;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constants.ANIM_IDLE);
        timer = 0f;
        maxTimer = Random.Range(2, 5);
    }

    public void OnExcute(Bot t)
    {
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {
        timer = 0f;
        maxTimer = 0f;
    }
}
