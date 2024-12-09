using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Object" , menuName = "Scriptable Objects/Spawn Object")]
public class SpawnData : ScriptableObject
{
    [Serializable]
    private struct EnnemiesSpawner
    {
        public PoolType enemy;
        [Range(-1,1)] public float xPosEnemy;
    }

    [Serializable]
    private struct CrowdSpawner
    {
        public CrowdData crowd;
        [SerializeField, Range(0,1)] public float zPosCrowd;
        public bool isLeft;
    }
    
    [SerializeField] private List<EnnemiesSpawner> ennemiesSpawners;
    [SerializeField] private List<CrowdSpawner> crowdSpawners;

    public int GetEnemiesCount() => ennemiesSpawners.Count;
    
    public PoolType GetEnemyType(int index) => ennemiesSpawners[index].enemy;
    public float GetEnemyXPos(int index) => ennemiesSpawners[index].xPosEnemy;
    
    public int GetCrowdCount() => crowdSpawners.Count;
    public CrowdData GetCrowd(int index) => crowdSpawners[index].crowd;
    public float GetCrowdZPos(int index) => crowdSpawners[index].zPosCrowd;
    public bool GetIsLeft(int index) => crowdSpawners[index].isLeft;
}