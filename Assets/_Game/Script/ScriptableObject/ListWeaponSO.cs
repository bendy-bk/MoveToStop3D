using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListWeapon", menuName = "SO/ListWeapon")]
[Serializable]
public class ListWeaponSO : ScriptableObject
{
    public List<WeaponData> weaponDatas = new List<WeaponData>();
}


[System.Serializable]
public class WeaponData
{
    public WeaponType weaponType;
    public string weaponName;
    public string weaponENG;
    public Sprite icon;
    public GameUnit prefab;
    public BulletBase bullet;
    public int damage;
    public int price;
    public float attackSpeed;
    public float range;
}