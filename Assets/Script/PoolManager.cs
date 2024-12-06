using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public enum PoolTypes
    {
        Projectile,
        Ennemies,
        Crowd
    }

    [Serializable]
    public class Pool
    {
        public PoolTypes type;
        public GameObject prefab;
        public int size;
    }
    
    [SerializeField] private List<Pool> pools;
    private Dictionary<PoolTypes, Queue<Spawnable>> poolDictionary = new();

    private void Start()
    {
        // Spawn all spawnables
        foreach (Pool pool in pools)
        {
            Queue<Spawnable> poolQueue = new();
            Spawnable spawnable;
            
            for (int loop = 0; loop < pool.size; loop++)
            {
                spawnable = Instantiate(pool.prefab, transform).GetComponent<Spawnable>();
                spawnable.Despawn();
                poolQueue.Enqueue(spawnable);
            }
            
            poolDictionary.Add(pool.type, poolQueue);
        }
    }

    private void FixedUpdate()
    {
        GetSpawnable(PoolTypes.Projectile);
    }

    public Spawnable GetSpawnable(PoolTypes type)
    {
        return GetSpawnable(type, Vector3.zero, Quaternion.identity);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public Spawnable GetSpawnable(PoolTypes type, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.TryGetValue(type, out Queue<Spawnable> poolQueue))
        {
            Debug.LogError("Type " + type + " does not exist");
            return null;
        }
        
        Spawnable spawnable = poolQueue.Dequeue();
        spawnable.Despawn();
        
        spawnable.transform.position = position;
        spawnable.transform.rotation = rotation;
        
        spawnable.gameObject.SetActive(true);
        spawnable.OnSpawn();
        
        poolQueue.Enqueue(spawnable);
        
        return spawnable;
    }
}

public abstract class Spawnable : MonoBehaviour
{
    public abstract void OnSpawn();
    public abstract void Despawn();
}