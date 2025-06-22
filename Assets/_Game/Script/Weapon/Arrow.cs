using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Character characterOwner;
    private Character targetCharacter;
    public float speed;
    private bool isFlying = false;
    public Rigidbody rb;
    public Vector3 directionAttack;

    public bool IsFlying { get => isFlying; set => isFlying = value; }

    void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (!IsFlying) return;

        // Di chuyển theo hướng cố định
        Vector3 movePosition = transform.position + directionAttack.normalized * speed * Time.deltaTime;
        rb.MovePosition(movePosition);

        // Hướng viên đạn
        transform.forward = directionAttack.normalized;

        float maxDistance = 10f;
        if (Vector3.Distance(characterOwner.TF.position, transform.position) > maxDistance)
        {
            OnDespawn(); 
        }
    }

    public void OnDespawn()
    {
        IsFlying = false;
        characterOwner.IsAttacking = false;
        characterOwner.IsThrowing = false;
        gameObject.SetActive(false); // Chỉ set false khi chạm
    }

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



}
