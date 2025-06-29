using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletBase : GameUnit
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed;

    [SerializeField] protected Character characterOwner;
    [SerializeField] protected Character characterTarget;
    [SerializeField] protected Vector3 directionAttack;

    [SerializeField] protected Transform targetPosition;

    void Update()
    {
        MoveToTarget();
    }

    public override void OnInit()
    {
    }

    public override void OnDespawn()
    {
        characterOwner.IsAttacking = false;
        SimplePool.Despawn(this);
    }

    public void MoveToTarget()
    {
        // Di chuyển theo hướng cố định
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition.position) <= 0.01f)
        {
            OnDespawn();
        }
    }

    public void SetTargetFly(Character owner, Character target, Vector3 dir)
    {
        characterTarget = target;
        characterOwner = owner;
        directionAttack = dir;

        targetPosition = target.TF;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character c = other.GetComponent<Character>();

            // Tránh tự va chạm chính mình
            if (c != null && c != characterOwner)
            {
                characterOwner?.OnHit();
                c.OnDeath();             // Mục tiêu chết
                characterOwner?.RemoveTarget(characterTarget); // Xoá khỏi danh sách
                OnDespawn();
            }
        }
    }
}
