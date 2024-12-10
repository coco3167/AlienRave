using UnityEngine;

[CreateAssetMenu(fileName = "InstantPowerUpData", menuName = "Scriptable Objects/PowerUp/InstantPowerUpData")]
public class InstantPowerUpData : PowerUpData
{
	public override PowerUp PowerUp => new InstantPowerUp(value,type);
}
