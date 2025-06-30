using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] private WeaponType weaponType;

    [SerializeField] private BulletBase bulletBase;

    private GameUnit preFap;

    private bool isEquipped;

    private Character characterOwner;

    private Character target;

    private Vector3 targetPos;

    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }
    public GameUnit PreFap { get => preFap; set => preFap = value; }
    public bool IsEquipped { get => isEquipped; set => isEquipped = value; }
    public Character CharacterOwner { get => characterOwner; set => characterOwner = value; }
    public Character Target { get => target; set => target = value; }

    public Weapon(WeaponSO wso)
    {
        WeaponType = wso.WeaponType;
        PreFap = wso.Prefab;
        IsEquipped = wso.IsEquipped;
        bulletBase = wso.Bullet;
    }

    public Weapon()
    {
    }

    public void Shoot()
    {
        BulletBase bullet = Instantiate(bulletBase, CharacterOwner.ThrowPoint.position, Quaternion.identity);

        bullet.SetTargetFly(CharacterOwner, target, targetPos);


    }

    public void SetCharacterowner(Character c, Character t, Vector3 targetPos)
    {
        CharacterOwner = c;
        Target = t;
        this.targetPos = targetPos;
    }


    public override void OnInit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDespawn()
    {
        throw new System.NotImplementedException();
    }
}
