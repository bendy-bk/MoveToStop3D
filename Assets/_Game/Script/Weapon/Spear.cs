using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spear : BulletBase
{
    public float rotateSpeed = 900f;
    protected override void RotateToTarget()
    {
        //transform.Rotate(new Vector3(1,0,0) * rotateSpeed * Time.deltaTime);
    }

    protected override void MoveToTarget()
    {
        // Di chuyển theo hướng cố định
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            OnDespawn();
        }
    }
}
