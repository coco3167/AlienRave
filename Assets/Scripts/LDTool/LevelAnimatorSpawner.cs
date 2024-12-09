using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
						StartCoroutine(SpawnFollowEnemies(type,
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

		public IEnumerator SpawnFollowEnemies(PoolType type, VertexPath path, int nb)
		{
			int nbSpawned = 0;
			while(nbSpawned < nb)
			{
				yield return new WaitForSeconds(0.2f);
				FollowEnemy enemy = PoolManager.Instance.SpawnElement(type,
							path.GetPointAtDistance(0),
							path.GetRotationAtDistance(0)) as FollowEnemy;
				enemy.StartFollowing(path);
				++nbSpawned;
			}
			yield return null;
		}
	}
}
