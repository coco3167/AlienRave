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
	private readonly Dictionary<PoolType, Queue<GameObject>> poolDictionary = new();

	private void Awake() => instance = this;

	private void Start()
	{
		// Spawn all spawnables
		foreach (Pool pool in pools)
		{
			Queue<GameObject> poolQueue = new();
			GameObject spawnable;
			
			for (int loop = 0; loop < pool.size; loop++)
			{
				spawnable = Instantiate(pool.prefab, transform);
				spawnable.SetActive(false);
				poolQueue.Enqueue(spawnable);
			}
			
			poolDictionary.Add(pool.type, poolQueue);
		}
	}
	
	/*private void FixedUpdate()
	{
		GetSpawnable(PoolTypes.Projectile);
	}*/

	public GameObject SpawnElement(PoolType type)
	{
		return SpawnElement(type, Vector3.zero, Quaternion.identity);
	}

	// ReSharper disable Unity.PerformanceAnalysis
	public GameObject SpawnElement(PoolType type, Vector3 position, Quaternion rotation)
	{
		if (!poolDictionary.TryGetValue(type, out Queue<GameObject> poolQueue))
		{
			Debug.LogError("Type " + type + " does not exist");
			return null;
		}

		GameObject spawnable = poolQueue.Dequeue();
		spawnable.transform.SetPositionAndRotation(position, rotation);
		spawnable.SetActive(true);
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
