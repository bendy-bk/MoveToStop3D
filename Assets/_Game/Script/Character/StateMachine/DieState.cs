using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DieState : IState<Bot>
{

    public void OnEnter(Bot t)
    {
        t.StopMove();

        t.ChangeAnim(Constants.ANIM_DEAD);

        t.StartCoroutine(WaitBeforeDespawn(t)); ;
    }

    public void OnExecute(Bot t){}

    public void OnExit(Bot t){}

    private IEnumerator WaitBeforeDespawn(Bot bot)
    {
        yield return new WaitForSeconds(2f);
        bot.OnDespawn();
    }

}

