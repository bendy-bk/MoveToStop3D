using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : GenericSingleton<EquipmentManager>
{
    //ListWeapon owner
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    public List<Weapon> Weapons { get => weapons; set => weapons = value; }

    private void Awake()
    {

    }

    public Weapon GetWeaponEquip()
    {
        foreach (var w in Weapons)
        {
            if (w.IsEquipped)
            {
                return w;
            }
        }
        return null; // Trường hợp không có vũ khí nào được trang bị
    }


    public void Equip(WeaponType weaponType)
    {

    }

}

