using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static SpawnData;
using Random = UnityEngine.Random;

namespace LDTool
{
	public class LevelAnimationSpawner : MonoBehaviour
	{
		[SerializeField] private Transform enemySpawnPoint;
		[SerializeField] private List<Transform> crowdSpawnPoint;
		[SerializeField] private CrowdManager crowdManager;

		[SerializeField] private List<PathCreator> enemyPaths;

		private float zLength;
		
		private void Start()
		{
			zLength = Mathf.Abs(crowdSpawnPoint[0].position.z - crowdSpawnPoint[1].position.z);
		}

		public void Spawn(SpawnData spawnObject)
		{
			if(spawnObject.HasEnemiesToSpawn)
			{
				for (int loop = 0; loop < spawnObject.GetEnemiesCount(); loop++)
				{
					PoolType type = spawnObject.GetEnemyType(loop);
					if (type == PoolType.GreenFollowEnemy || type == PoolType.PinkFollowEnemy)
					{
						var followMode = spawnObject.GetEnemyFollowMode(loop);

						StartCoroutine(SpawnFollowEnemies(followMode,
							enemyPaths[Random.Range(0, enemyPaths.Count)].path,
							spawnObject.GetNbEnemies(loop)));
					}
					else
					{
						Vector3 pos = enemySpawnPoint.position;
						pos.x *= spawnObject.GetEnemyXPos(loop);
						PoolManager.Instance.SpawnElement(type, pos, enemySpawnPoint.rotation);
					}
				}
			}

			if(spawnObject.HasCrowdToSpawn)
			{
				for (int loop = 0; loop < spawnObject.GetCrowdCount(); loop++)
				{
					CrowdData crowd = spawnObject.GetCrowd(loop);

					float xPos = spawnObject.GetIsLeft(loop) ? crowdSpawnPoint[0].position.x : crowdSpawnPoint[1].position.x;
					Vector3 obstacleDirection = spawnObject.GetIsLeft(loop) ? Vector3.right : Vector3.left;
					crowdManager.SpawnCrowdObstacles(crowd, xPos, spawnObject.GetCrowdZPos(loop), crowdSpawnPoint[0].position.z, zLength, obstacleDirection);
				}
			}
		}

		public IEnumerator SpawnFollowEnemies(FollowMode followMode, VertexPath path, int nb)
		{
			PoolType type = RandomFollowEnemy;
			if (followMode == FollowMode.PinkOnly) type = PoolType.PinkFollowEnemy;
			if (followMode == FollowMode.GreenOnly) type = PoolType.GreenFollowEnemy;

			int nbSpawned = 0;
			while(nbSpawned < nb)
			{
				if(followMode == FollowMode.Alternate)
					type = type == PoolType.PinkFollowEnemy ? PoolType.GreenFollowEnemy : PoolType.PinkFollowEnemy;
				if (followMode == FollowMode.Random) type = RandomFollowEnemy;

				yield return new WaitForSeconds(0.2f);
				FollowEnemy enemy = PoolManager.Instance.SpawnElement(type,
							path.GetPointAtDistance(0),
							path.GetRotationAtDistance(0)) as FollowEnemy;
				enemy.StartFollowing(path);
				++nbSpawned;
			}
			yield return null;
		}

		private PoolType RandomFollowEnemy => 
			Random.Range(0, 2) == 0 ? PoolType.PinkFollowEnemy : PoolType.GreenFollowEnemy;
		
		public void EndLevel()
		{
			GameManager.Instance.WinLevel();
		}
	}
}
