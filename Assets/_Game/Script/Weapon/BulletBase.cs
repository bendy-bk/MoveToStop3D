using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BulletBase : GameUnit
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed;
    protected Character characterOwner;
    protected Character characterTarget;
    protected Vector3 directionAttack;
    protected Vector3 targetPosition;

    void Update()
    {
        MoveToTarget();
        RotateToTarget();
    }

    public override void OnInit()
    {
    }

    public override void OnDespawn()
    {
        characterOwner.FixedTarget = Vector3.zero;
        SimplePool.Despawn(this);
    }

    protected virtual void RotateToTarget() { }

    protected virtual void MoveToTarget() { }

    public void SetTargetFly(Character owner, Character target, Vector3 pos)
    {
        characterTarget = target;
        characterOwner = owner;
        targetPosition = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character c = other.GetComponent<Character>();
            characterOwner?.RemoveTarget(characterTarget); // Xoá khỏi danh sách
            // Tránh tự va chạm chính mình
            if (c != null && c != characterOwner)
            {
                if (c is Player player)
                {
                    player.OnDespawn();
                }
                else
                if (c is Bot bot)
                {
                    bot.ChangeState(new DieState());
                }

                characterOwner?.OnHit();
                //Despawn bullet
                OnDespawn();
            }
        }
    }
}
