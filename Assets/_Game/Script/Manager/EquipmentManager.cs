using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : GenericSingleton<EquipmentManager>
{

    [SerializeField] private List<WeaponSO> weaponSOs = new List<WeaponSO>();

    public List<WeaponSO> WeaponSOs { get => weaponSOs; set => weaponSOs = value; }


    public WeaponSO GetWeapon(WeaponType type)
    {
        return weaponSOs.Where(_ => _.WeaponType == type).FirstOrDefault();
    }

    public WeaponSO GetWeaponEquip()
    {
        return weaponSOs.Where(_ => _.IsEquipped == true & _.Unlock == true).FirstOrDefault();
    }

    public void Equip(WeaponType weaponType)
    {

    }

}

