
using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Bot : Character
{
    public static event Action<Bot> OnBotDeathBot;
    private Transform spawmPoint;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject circleTarget;
    private Vector3 destionation;

    public GameObject CircleTarget { get => circleTarget; set => circleTarget = value; }
    public StateMachine<Bot> StateMachine { get; private set; } = new StateMachine<Bot>();
    public bool IsDestination => Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;

    public Transform SpawmPoint { get => spawmPoint; set => spawmPoint = value; }

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            StateMachine.Update(this);
        }
    }

    public override void OnInit()
    {
        StateMachine.ChangeState(this, new IdleState());
        WeaponEquip = EquipmentManager.Instance.GetWeaponEquip();
        Instantiate(WeaponEquip, ThrowPoint.position, Quaternion.identity, ThrowPoint);
    }

    public override void OnDespawn()
    {
        RemoveBotDeaded += HandleTargetDeath;    
        OnBotDeathBot?.Invoke(this);
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        //destionation = position;
        //destionation.y = 0;
        agent.SetDestination(position);
    }

    internal void StopMove()
    {
        agent.enabled = false;
    }

    internal void StartMove()
    {
        agent.enabled = true;
    }

    internal void FindTarget()
    {
        TargetCharacter = FindClosestTarget();
    }

    internal void MoveToTarget()
    {
        SetDestination(TargetCharacter.TF.position);
    }

    internal bool IsTargetInAttackRange()
    {
        if (TargetCharacter == null) return false;
        return Vector3.Distance(transform.position, TargetCharacter.TF.position) <= 3f;
    }

    public void ChangeState(IState<Bot> newState)
    {
        StateMachine.ChangeState(this, newState);
    }

    public Character FindClosestTarget()
    {
        float minDist = float.MaxValue;
        Character closest = null;

        foreach (var player in LevelManager.Instance.totalCharacters)
        {
            // Loại bỏ chính bản thân mình
            if (player == this) continue;

            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = player;
            }
        }

        return closest;
    }

}
