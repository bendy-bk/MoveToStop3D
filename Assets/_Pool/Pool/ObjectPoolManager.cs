using System.Collections.Generic;
using UnityEngine;

public enum PoolType { Bot = 0, Player = 1, Bullet = 2 }

public class ObjectPoolManager : GenericSingleton<ObjectPoolManager>
{
    [System.Serializable]
    public class Pool
    {
        public PoolType PoolType;
        public GameObject prefab;
        public int amount;
    }

    public List<Pool> pools;

    private Dictionary<PoolType, Queue<GameObject>> poolDictionary;
    private Dictionary<PoolType, Transform> poolParents; // NEW

    private void Awake()
    {
        poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();
        poolParents = new Dictionary<PoolType, Transform>();

        foreach (Pool pool in pools)
        {
            // Tạo parent object để chứa loại pool này
            GameObject parentObject = new GameObject(pool.PoolType.ToString() + "Pool");
            parentObject.transform.SetParent(this.transform); // giữ gọn dưới ObjectPoolManager
            poolParents.Add(pool.PoolType, parentObject.transform);

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.amount; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(parentObject.transform); // Gắn vào parent
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.PoolType, objectPool);
        }
    }

    public GameObject SpawnFromPool(PoolType tag, Transform Point)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();

        objToSpawn.transform.position = Point.position;

        objToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}
