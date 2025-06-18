
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
        if (JoystickControl.direct != Vector3.zero)
        {
            isMove = true;
            Vector3 direction = JoystickControl.direct;
            Vector3 movement = direction * speed * Time.deltaTime;

            transform.position += movement;

            if (model != null)
                model.forward = direction;
            ChangeAnim(Constants.ANIM_RUN);

        }
        else if(JoystickControl.direct == Vector3.zero)
        {
            isMove = false;

            if (CharacterCount > 0 && !isAttack)
            {      
                if (!isThrowing)
                {
                    ChangeAnim(Constants.ANIM_ATTACK);
                    isAttack = true;
                    Attack();
                }
              
            }
            else if (CharacterCount == 0)
            {
                ChangeAnim(Constants.ANIM_IDLE);
            }
        }
    }




}

