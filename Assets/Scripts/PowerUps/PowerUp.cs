using UnityEngine;

public abstract class PowerUp
{
	protected float value;
	protected PowerUpData.Type type;

	public PowerUp(float value, PowerUpData.Type type)
	{
		this.value = value;
		this.type = type;
	}

	public abstract Sprite Apply(PlayerData playerData, PlayerController player);
}
