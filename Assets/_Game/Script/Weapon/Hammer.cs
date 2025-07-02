using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Hammer : BulletBase
{
    public float rotateSpeed = 900f;
    protected override void RotateToTarget()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime); // Nếu trục X là mặt búa
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
