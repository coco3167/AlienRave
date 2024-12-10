using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/Projectile/WaveData")]
public class WaveData : ProjectileData
{
	public float minRadius = 2;
	public float maxRadius = 20;
}
