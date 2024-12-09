using UnityEngine;

[CreateAssetMenu(fileName = "CrowdData", menuName = "Scriptable Objects/CrowdData")]
public class CrowdData : ScriptableObject
{
    public PoolType PoolType { get; private set; } = PoolType.CrowdObstacle;
    public int nb;
    public int length;
    public float speed;
    [Range(0, 1)] public float zSpread;
}
