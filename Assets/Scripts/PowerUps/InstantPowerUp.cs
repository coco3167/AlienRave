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
				break;
			case PowerUpData.Type.NbProjectiles: playerData.nbProjectiles += (int)value;
				break;
			case PowerUpData.Type.Homing: //TODO todo? 
				break;
		}

		return null;
	}
}
