using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDTool
{
    public class LevelAnimationSpawner : MonoBehaviour
    {
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private List<Transform> crowdSpawnPoint;
        [SerializeField] private CrowdManager crowdManager;

        private float zLength;
        
        private void Start()
        {
            zLength = Mathf.Abs(crowdSpawnPoint[0].position.z - crowdSpawnPoint[1].position.z);
        }

        public void Spawn(SpawnData spawnObject)
        {
            for (int loop = 0; loop < spawnObject.GetEnemiesCount(); loop++)
            {
                PoolType enemy  = spawnObject.GetEnemyType(loop);
                
                Vector3 newPos = new Vector3(spawnObject.GetEnemyXPos(loop) * enemySpawnPoint.position.x, enemySpawnPoint.position.y, enemySpawnPoint.position.z);
                PoolManager.Instance.SpawnElement(enemy, newPos, enemySpawnPoint.rotation);
            }

            for (int loop = 0; loop < spawnObject.GetCrowdCount(); loop++)
            {
                CrowdData crowd = spawnObject.GetCrowd(loop);

                float xPos = spawnObject.GetIsLeft(loop) ? crowdSpawnPoint[0].position.x : crowdSpawnPoint[1].position.x;
                Vector3 obstacleDirection = spawnObject.GetIsLeft(loop) ? Vector3.right : Vector3.left;
                crowdManager.SpawnCrowdObstacles(crowd, xPos, spawnObject.GetCrowdZPos(loop), crowdSpawnPoint[0].position.z, zLength, obstacleDirection);
            }
        }
    }
}
