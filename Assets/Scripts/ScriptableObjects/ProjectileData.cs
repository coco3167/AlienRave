using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/Projectile/ProjectileData")]
public class ProjectileData : ScriptableObject
{
	public int damage;
	public float speed;
	public string targetTag;
}
