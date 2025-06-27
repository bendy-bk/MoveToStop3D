
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public GameObject c;

    public NavMeshAgent agent;

    private Vector3 destionation;

    private IState<Bot> currentState;

    public bool IsDestination => Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;

    private void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        // To do : random weapon for bot
        Instantiate(c, ThrowPoint.position, Quaternion.identity, ThrowPoint);
    }

    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) && currentState != null)
        {
            if (currentState != null)
            {
                currentState.OnExcute(this);
            }
        }
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

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void StopMove()
    {
        agent.enabled = false;
    }

    public Vector3 RandomPosGround()
    {
        float x = UnityEngine.Random.Range(-20f, 20f); // nửa chiều dài
        float z = UnityEngine.Random.Range(-20f, 20f); // nửa chiều rộng
        float y = 0f; // mặt đất

        Vector3 randomPos = new Vector3(x, y, z);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, 1f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

}
