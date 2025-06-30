using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
        characterOwner.IsAttacking = false;
        gameObject.SetActive(false);
        characterOwner.FixedTarget = Vector3.zero;
        //SimplePool.Despawn(this);
    }

    protected virtual void RotateToTarget()
    {
        // Mặc định không làm gì
    }

    public void MoveToTarget()
    {
        Debug.Log(targetPosition);
        // Di chuyển theo hướng cố định
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            OnDespawn();
        }
    }

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
