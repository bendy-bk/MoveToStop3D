using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : GenericSingleton<EquipmentManager>
{
    //ListWeapon owner
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    //List Weapon savefile
    [SerializeField] private List<WeaponDTO> weaponDTOs;

    [SerializeField] private ListWeaponSO listWeaponSO;

    private void Awake()
    {
        LoadDataUserWeapon();
    }

    public List<Weapon> Weapons { get => weapons; set => weapons = value; }

    public ListWeaponSO ListWeaponSO { get => listWeaponSO; set => listWeaponSO = value; }

    public List<WeaponDTO> WeaponDTOs { get => weaponDTOs; set => weaponDTOs = value; }

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

    public void LoadDataUserWeapon()
    {
        WeaponDTOs = UserDataManager.Instance.UserData.weaponDTOs;
    }

    internal void UnlockWeapon(WeaponType type)
    {
        WeaponDTO w = GetWeaponByType(type);

        WeaponDTOs.Add(w);

        UserDataManager.Instance.SaveData();
    }

    private WeaponDTO GetWeaponByType(WeaponType type)
    {
        WeaponData wa = ListWeaponSO.WeaponDatas.FirstOrDefault(_=> _.weaponType == type);

        WeaponDTO w = new WeaponDTO(type, false);      

        return w;
    }

    internal void EquipWeapon(WeaponType type)
    {
        foreach (var item in WeaponDTOs)
        {
            item.isEquipped = false;

            if (item.weaponType == type)
            {
                item.isEquipped = true;
            }
        }
    }


}

[Serializable]
public class WeaponDTO
{
    public WeaponType weaponType;
    public bool isEquipped;

    public WeaponDTO(WeaponType weaponType, bool isEquipped)
    {
        this.weaponType = weaponType;
        isEquipped = false;
    }

    public WeaponDTO(WeaponType weaponType)
    {
        this.weaponType = weaponType;
    }
}
