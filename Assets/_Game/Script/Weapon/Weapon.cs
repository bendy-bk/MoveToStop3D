using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Weapon : GameUnit
{
    [SerializeField] private WeaponType weaponType;

    [SerializeField] private BulletBase bulletBase;

    [SerializeField] private bool isEquipped;

    private Character characterOwner;

    private Character target;

    private Vector3 targetPos;

    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }
    public bool IsEquipped { get => isEquipped; set => isEquipped = value; }
    public Character CharacterOwner { get => characterOwner; set => characterOwner = value; }
    public Character Target { get => target; set => target = value; }
    public BulletBase BulletBase { get => bulletBase; set => bulletBase = value; }

    public async void Shoot()
    {

        //BulletBase bullet = Instantiate(bulletBase, CharacterOwner.ThrowPoint.position, Quaternion.identity);
        //Debug.Log("Player:" + CharacterOwner.ThrowPoint.position);
        var bullet = SimplePool.Spawn<BulletBase>(BulletBase.poolType, CharacterOwner.ThrowPoint.position, Quaternion.identity);
        bullet.TF.rotation = Quaternion.Euler(90, AngelFly(), 0);
        bullet.gameObject.SetActive(false);
        await Task.Delay(100);
        //bullet.TF.position = CharacterOwner.ThrowPoint.position;
        bullet.gameObject.SetActive(true);
        //Debug.Log("Bullet:" + bullet.TF.position);
        bullet.SetTargetFly(CharacterOwner, target, targetPos);

    }

    public void SetCharacterowner(Character c, Character t, Vector3 targetPos)
    {
        CharacterOwner = c;
        Target = t;
        this.targetPos = targetPos;
    }

    public float AngelFly()
    {
         Vector3 dirAt = targetPos - CharacterOwner.ThrowPoint.position;
         float angleY = Mathf.Atan2(dirAt.x, dirAt.z) * Mathf.Rad2Deg;
         return angleY;
    }

    public override void OnInit()
    {
        
    }

    public override void OnDespawn()
    {
        
    }
}
