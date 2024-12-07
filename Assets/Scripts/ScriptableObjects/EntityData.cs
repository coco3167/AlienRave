using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
	public float speed;
	public int maxHealth;
	public float fireRate;
	public int damage;
	public PoolType projectileType;
}
