using UnityEngine;

public class IdleState : IState<Bot>
{
    private float timer;
    private float maxTimer;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constants.ANIM_IDLE);
        timer = 0f;
        maxTimer = Random.Range(1,7);
    }

    public void OnExecute(Bot t)
    {
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            t.ChangeState(new DetectState());
        }
    }

    public void OnExit(Bot t)
    {
        timer = 0f;
        maxTimer = 0f;
    }
}
