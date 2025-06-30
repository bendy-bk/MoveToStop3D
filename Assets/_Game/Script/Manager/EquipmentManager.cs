using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : GenericSingleton<EquipmentManager>
{

    [SerializeField] private List<WeaponSO> weaponSOs = new();

    //ListWeapon owner
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    public List<WeaponSO> WeaponSOs => weaponSOs;

    public List<Weapon> Weapons { get => weapons; set => weapons = value; }

    private void Awake()
    {
        weaponSOs = Resources.LoadAll<WeaponSO>("WeaponSO").ToList();
        //To do: load file
        //GetListWeaponUnlock();
    }


    public void GetListWeaponUnlock()
    {
        foreach (var weaponso in WeaponSOs)
        {
            if(weaponso.Unlock)
            {
                Weapon w = new Weapon(weaponso);
                Weapons.Add(w);
            }
        }
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

