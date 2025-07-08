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

        }
        else if (JoystickControl.direct == Vector3.zero)
        {
            IsMoving = false;

            if (CharacterCount > 0)
            {
                Attack(); // gọi attack một lần duy nhất mỗi chu kỳ
            }
            else
            {
                ChangeAnim(Constants.ANIM_IDLE);
            }
        }
    }

    public override void OnInit()
    {
        TargetCharacter = null;
        transform.localScale = Vector3.one;
        WeaponEquip = EquipmentManager.Instance.GetWeaponEquip();

        GameUnit u = Instantiate(WeaponEquip, Vector3.zero, Quaternion.identity, ThrowPoint);
        u.TF.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        //GameUnit u = SimplePool.Spawn<Weapon>(PoolType.Bullet, ThrowPoint.position, Quaternion.identity);
        //u.transform.SetParent(ThrowPoint);

    }

}

