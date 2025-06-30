using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5;
    
    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            Move();
        }
    }
    public override void Move()
    {
        if (JoystickControl.direct != Vector3.zero)
        {
            IsMoving = true;
            Vector3 direction = JoystickControl.direct;
            Vector3 movement = direction * speed * Time.deltaTime;

            transform.position += movement;

            if (Model != null)
                Model.forward = direction;
            ChangeAnim(Constants.ANIM_RUN);

            // Nếu đang di chuyển mà có coroutine -> stop nó
            if (throwCoroutine != null)
            {
                StopCoroutine(throwCoroutine);
                throwCoroutine = null;
            }

        }
        else if (JoystickControl.direct == Vector3.zero)
        {
            IsMoving = false;

            if (CharacterCount > 0 && !IsAttacking)
            {
                Attack();
            }
            else if (CharacterCount == 0)
            {
                ChangeAnim(Constants.ANIM_IDLE);
            }
        }
    }

    public override void OnInit()
    {
        WeaponEquip = EquipmentManager.Instance.GetWeaponEquip();

        Instantiate(WeaponEquip.PreFap, ThrowPoint.position, Quaternion.identity, ThrowPoint);

        //GameUnit u = SimplePool.Spawn<Weapon>(PoolType.Bullet, ThrowPoint.position, Quaternion.identity);
        //u.transform.SetParent(ThrowPoint);

    }

}

