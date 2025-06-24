
using System.Collections.Generic;
using UnityEngine;


public static class SimplePool
{
    private static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();

    public static void PreLoad(GameUnit prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.Log("prefab null"); return;
        }

        // Chua co prefab hoac pool dang null 
        if (!poolInstance.ContainsKey(prefab.poolType) || poolInstance[prefab.poolType] == null)
        {
            Pool pool = new Pool();
            pool.PreLoad(prefab, amount, parent);
            poolInstance[prefab.poolType] = pool;
        }
    }


    public static T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.Log("not preload");
            return null;
        }

        return poolInstance[poolType].Spawn(pos, rot) as T;
    }


    public static void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.poolType))
        {
            Debug.Log("not preload des");
        }

        poolInstance[unit.poolType].Despawn(unit);
    }

}

public class Pool
{
    Transform parent;
    GameUnit prefab;
    //List chua cac unit dang o trong pool
    Queue<GameUnit> inactives = new Queue<GameUnit>();
    //List chua cac unit dang duoc su dung
    List<GameUnit> actives = new List<GameUnit>();


    public void PreLoad(GameUnit prefab, int amount, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < amount; i++)
        {
            //Sinh ra unit roi des no ve pool
            Despawn((GameUnit)GameObject.Instantiate(prefab, parent));
        }

    }


    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;

        if (inactives.Count <= 0)
        {
            unit = (GameUnit)GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives.Dequeue();
            unit.gameObject.SetActive(true);
        }
        unit.TF.SetLocalPositionAndRotation(pos, rot);

        actives.Add(unit);

        return unit;

    }

    //Tra phan tu ve pool
    public void Despawn(GameUnit unit)
    {
        // Dua ve pool inactives
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }

    //Thu thap all dua ve pool
    public void Collect()
    {
        while (actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }


    //Destroy all element
    public void Release()
    {
        Collect();
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }

        inactives.Clear();
    }
}

