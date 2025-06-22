using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class PatrolState : IState<Bot>
{
    UnityEngine.Vector3 point;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constants.ANIM_RUN);
        point = t.RandomPosGround();
    }

    public void OnExcute(Bot t)
    {
        t.Model.forward = point - t.TF.position;
        t.SetDestination(point);

        if(t.IsDestination)
        {
            t.ChangeState(new IdleState());
        }

        if(t.CharacterCount > 0)
        {
            t.StopMove();
            t.ChangeState(new AttackState());
        } 
    }

    public void OnExit(Bot t)
    {
        
    }

    
}

