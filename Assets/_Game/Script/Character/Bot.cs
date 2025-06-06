using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;

    private Vector3 destionation;

    public bool IsDestination => Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0;
        agent.SetDestination(position);
    }


}
