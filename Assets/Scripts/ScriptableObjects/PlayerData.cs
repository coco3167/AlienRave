using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
	public int playerIndex;
	public float speed = 5f;
	public float fireRate = 0.2f;
	public int nbProjectiles = 1;
	public float invulnerabilityDuration = 0.2f;
	public PoolType projType;
	public ProjectileData projectileData;
	public Queue<LastingPowerUp> powerUps;
}
