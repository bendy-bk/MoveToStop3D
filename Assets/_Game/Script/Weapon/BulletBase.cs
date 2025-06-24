using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : GameUnit
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed;

    protected Character characterOwner;
    protected Character targetCharacter; 
    protected bool isFlying = false;
    protected Vector3 directionAttack;

    public bool IsFlying { get => isFlying; set => isFlying = value; }

    public void SetTargetFly(Character owner, Character target, Vector3 dir)
    {
        targetCharacter = target;
        characterOwner = owner;
        directionAttack = dir;
        IsFlying = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character c = other.GetComponent<Character>();
            if (c != null)
            {
                characterOwner?.OnHit();
                c.OnDeath();
                // Xoá khỏi danh sách tấn công của người bắn
                characterOwner?.RemoveTarget(targetCharacter);
            }

            OnDespawn();
        }
    }

    public override void OnInit()
    {
    }

    public override void OnDespawn()
    {
        IsFlying = false;
        characterOwner.IsAttacking = false;
        characterOwner.IsThrowing = false;
        SimplePool.Despawn(this);
    }

}
