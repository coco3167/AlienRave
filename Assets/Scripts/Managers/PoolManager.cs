using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	#region Singleton

	private static PoolManager instance;
	public static PoolManager Instance => instance; // TODO faire un vrai singleton
	#endregion

	[Serializable]
	public class Pool
	{
		public PoolType type;
		public GameObject prefab;
		public int size;
	}
	
	[SerializeField] private List<Pool> pools;
	private readonly Dictionary<PoolType, Queue<Spawnable>> poolDictionary = new();

	private void Awake() => instance = this;

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

	public Spawnable SpawnElement(PoolType type)
	{
		return SpawnElement(type, Vector3.zero, Quaternion.identity);
	}

	// ReSharper disable Unity.PerformanceAnalysis
	public Spawnable SpawnElement(PoolType type, Vector3 position, Quaternion rotation)
	{
		if (!poolDictionary.TryGetValue(type, out Queue<Spawnable> poolQueue))
		{
			Debug.LogError("Type " + type + " does not exist");
			return null;
		}

		Spawnable spawnable = poolQueue.Dequeue();
		spawnable.transform.SetPositionAndRotation(position, rotation);
		spawnable.Spawn();
		poolQueue.Enqueue(spawnable);

		return spawnable;
	}
}

public enum PoolType
{
	PlayerPinkProjectile, PlayerGreenProjectile,
	PinkDrunkEnemy, GreenDrunkEnemy, PinkThrowEnemy, GreenThrowEnemy, HybridEnemy,
	CrowdMember
}
