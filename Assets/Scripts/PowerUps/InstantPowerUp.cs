using UnityEngine;

public class InstantPowerUp : PowerUp
{
	public InstantPowerUp(float value, PowerUpData.Type type) : base(value, type) {}

	public override Sprite Apply(PlayerData playerData, PlayerController player)
	{
		switch(type)
		{
			case PowerUpData.Type.Heal: GameManager.Instance.Heal((int)value);
				break;
			case PowerUpData.Type.FireRate: playerData.fireRate -= value;
				if (playerData.fireRate <= 0) playerData.fireRate = 0.01f;
				break;
			case PowerUpData.Type.NbProjectiles: player.IncreaseShootPoints();
				break;
			case PowerUpData.Type.Homing: //TODO todo? 
				break;
		}

		return null;
	}
}
