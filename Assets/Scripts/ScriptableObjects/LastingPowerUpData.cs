using UnityEngine;

[CreateAssetMenu(fileName = "LastingPowerUpData", menuName = "Scriptable Objects/LastingPowerUpData")]
public class LastingPowerUpData : PowerUpData
{
	public float duration;

	public Sprite sprite;
	public override PowerUp PowerUp => new LastingPowerUp(value,type,duration,sprite);
}
