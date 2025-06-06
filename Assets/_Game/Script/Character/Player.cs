
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5;

    private void Update()
    {
        Move();
    }


    public override void Move()
    {
        // Nếu joystick có hướng
        if (JoystickControl.direct != Vector3.zero)
        {

            Vector3 direction = JoystickControl.direct;
            Vector3 movement = direction * speed * Time.deltaTime;

            transform.position += movement;

            // Xoay nhân vật theo hướng di chuyển
            if (model != null)
                model.forward = direction;

            ChangeAnim(Constants.ANIM_RUN);
        }

        else
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }



}

