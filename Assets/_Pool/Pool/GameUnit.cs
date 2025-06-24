using UnityEngine;

public abstract class GameUnit : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            tf = tf ?? gameObject.transform;
            return tf;
        }
    }

    public PoolType poolType;

    public abstract void OnInit();
    public abstract void OnDespawn();

}

public enum PoolType
{
    Bot = 0, 
    Player = 1, 
    Bullet_Spear = 2,
    Bullet_Hammer = 3
}