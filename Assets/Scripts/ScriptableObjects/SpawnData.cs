using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Object" , menuName = "Scriptable Objects/Spawn/Spawn Object")]
public class SpawnData : ScriptableObject
{
	[Serializable]
	private struct EnemiesSpawner
	{
		public PoolType enemy;
		[Range(-1,1)] public float xPosEnemy;
		public int nb;
		public FollowMode followMode;
		public bool hasPowerUp;
		public PoolType powerUpType;
	}

	[Serializable]
	private struct CrowdSpawner
	{
		public CrowdData crowd;
		[SerializeField, Range(0,1)] public float zPosCrowd;
		public bool isLeft;
	}
	
	[SerializeField] private List<EnemiesSpawner> enemiesSpawners;
	[SerializeField] private List<CrowdSpawner> crowdSpawners;

	public int GetEnemiesCount() => enemiesSpawners.Count;
	
	public PoolType GetEnemyType(int index) => enemiesSpawners[index].enemy;
	public float GetEnemyXPos(int index) => enemiesSpawners[index].xPosEnemy;

	public int GetNbEnemies(int index) => enemiesSpawners[index].nb;

	public FollowMode GetEnemyFollowMode(int index) => enemiesSpawners[index].followMode;

	public int GetCrowdCount() => crowdSpawners.Count;
	public CrowdData GetCrowd(int index) => crowdSpawners[index].crowd;
	public float GetCrowdZPos(int index) => crowdSpawners[index].zPosCrowd;
	public bool GetIsLeft(int index) => crowdSpawners[index].isLeft;

	public bool TryGetEnemyPowerUp(int index, out PoolType powerUpType)
	{
		powerUpType = enemiesSpawners[index].powerUpType;
		return enemiesSpawners[index].hasPowerUp;
	}

	public bool HasCrowdToSpawn => crowdSpawners != null;
	public bool HasEnemiesToSpawn => enemiesSpawners != null;

	public enum FollowMode { PinkOnly, GreenOnly, Alternate, Random }
}