using UnityEngine;

[CreateAssetMenu(fileName = "InstantPowerUpData", menuName = "Scriptable Objects/InstantPowerUpData")]
public class InstantPowerUpData : PowerUpData
{
	public override PowerUp PowerUp => new InstantPowerUp(value,type);
}
