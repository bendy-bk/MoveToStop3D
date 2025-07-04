using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListWeapon", menuName = "SO/ListWeapon")]
[Serializable]
public class ListWeaponSO : ScriptableObject
{
    [SerializeField] private List<WeaponData> weaponDatas = new List<WeaponData>();

    public List<WeaponData> WeaponDatas { get => weaponDatas; set => weaponDatas = value; }
}


[System.Serializable]
public class WeaponData
{
    public WeaponType weaponType;
    public string weaponName;
    public string weaponENG;
    public Sprite icon;
    public Weapon prefab;
    public BulletBase bullet;
    public int damage;
    public int price;
    public float attackSpeed;
    public float range;
}