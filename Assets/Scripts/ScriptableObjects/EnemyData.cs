using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Entity/Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
	public float speed;
	public int maxHealth;
	public int damage;

	[Range(0, 1)]
	public float powerUpDropChance;
}
