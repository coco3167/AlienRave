using UnityEngine;

[CreateAssetMenu(fileName = "ThrowEnemyData", menuName = "Scriptable Objects/ThrowEnemyData")]
public class ThrowEnemyData : EnemyData
{
	public float shootCooldown = 1f;
	public float salvoCooldown = 2f;
	public int nbProjPerSalvo = 3;
	public int nbSalvoBeforeMove = 2;
	public float lateralSpeed;
	public PoolType projectileType;
}
