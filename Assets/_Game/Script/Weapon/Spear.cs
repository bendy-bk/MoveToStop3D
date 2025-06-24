using System;
using UnityEngine;

public class Spear : BulletBase
{
    void Update()
    {
        MoveToTarget();
    }
    public void MoveToTarget()
    {
        if (!IsFlying) return;

        // Di chuyển theo hướng cố định
        Vector3 movePosition = transform.position + directionAttack.normalized * speed * Time.deltaTime;

        rb.MovePosition(movePosition);

        // Hướng viên đạn
        transform.forward = directionAttack.normalized;

        float maxDis = 6f;

        if (Vector3.Distance(characterOwner.TF.position, transform.position) > maxDis)
        {
            OnDespawn();
        }
    }
}
