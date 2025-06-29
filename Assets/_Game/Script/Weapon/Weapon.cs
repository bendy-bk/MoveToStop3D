using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : GameUnit
{

    [SerializeField] private WeaponType weaponType;

    [SerializeField] private GameUnit preFap;

    [SerializeField] private bool isEquipped;

    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }
    public GameUnit PreFap { get => preFap; set => preFap = value; }
    public bool IsEquipped { get => isEquipped; set => isEquipped = value; }

    public Weapon(WeaponSO wso)
    {
        WeaponType = wso.WeaponType;
        PreFap = wso.Prefab;
        IsEquipped = wso.IsEquipped;
    }

    public Weapon()
    {
    }

    public override void OnDespawn()
    {
    }

    public override void OnInit()
    {
    }

    public void Shoot()
    {

    }

}
