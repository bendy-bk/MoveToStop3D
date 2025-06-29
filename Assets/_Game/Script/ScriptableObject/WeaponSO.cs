using System;
using UnityEngine;



[CreateAssetMenu(fileName = "NewWeapon", menuName = "SO/Weapon")]
[Serializable]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private string weaponName;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameUnit prefab;
    [SerializeField] private int damage;
    [SerializeField] private int price;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float range;
    [SerializeField] private bool unlock;
    [SerializeField] private bool isEquipped;

    public string WeaponName { get => weaponName; set => weaponName = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public GameUnit Prefab { get => prefab; set => prefab = value; }
    public int Damage { get => damage; set => damage = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Range { get => range; set => range = value; }
    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }
    public bool Unlock { get => unlock; set => unlock = value; }
    public bool IsEquipped { get => isEquipped; set => isEquipped = value; }
    public int Price { get => price; set => price = value; }
}
public enum WeaponType
{
    Spear,
    Hammer,
    Boomerang,
}