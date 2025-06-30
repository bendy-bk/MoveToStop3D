using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Hammer : BulletBase
{
    public float rotateSpeed = 900f;
    protected override void RotateToTarget()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
