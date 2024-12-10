using UnityEngine;

[CreateAssetMenu(fileName = "LastingPowerUpData", menuName = "Scriptable Objects/PowerUp/LastingPowerUpData")]
public class LastingPowerUpData : PowerUpData
{
	public float duration;
	public override PowerUp PowerUp => new LastingPowerUp(value,type,duration);
}
