using UnityEngine;

public class InstantPowerUp : PowerUp
{
	public InstantPowerUp(float value, PowerUpData.Type type) : base(value, type) {}

	public override void Apply()
	{
		switch(type)
		{
			case PowerUpData.Type.Heal: GameManager.Instance.Heal((int)value);
				break;
			case PowerUpData.Type.FireRate: GameManager.Instance.IncreaseFireRate(value);
				break;
			case PowerUpData.Type.NbProjectiles: GameManager.Instance.IncreaseNbProjectiles((int)value);
				break;
		}
	}
}
