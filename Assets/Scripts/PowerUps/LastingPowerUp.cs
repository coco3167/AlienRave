using UnityEngine;

public class LastingPowerUp : PowerUp
{
	private PlayerData playerData;
	private readonly float duration;
	public Sprite sprite;

	public LastingPowerUp(float value, PowerUpData.Type type, float duration, Sprite sprite) : base(value, type)
	{
		this.duration = duration;
		this.sprite = sprite;
	}

	public override Sprite Apply(PlayerData playerData, PlayerController player)
	{
		this.playerData = playerData;

		switch (type)
		{
			case PowerUpData.Type.Speed: playerData.speed += value;
				break;

			case PowerUpData.Type.Damage: playerData.projectileData.damage += (int)value;
				break;

			case PowerUpData.Type.Invulnerability:  playerData.invulnerabilityDuration += duration;
				player.Invulnerability();
				break;

			case PowerUpData.Type.SlowMotion: //TODO todo?
				break;
		}

		playerData.powerUps.Enqueue(this);
		player.StartCoroutine(player.PowerUpTimer(duration));
		return sprite;
	}

	public void Remove()
	{
		switch (type)
		{
			case PowerUpData.Type.Speed:playerData.speed -= value;
				break;

			case PowerUpData.Type.Damage: playerData.projectileData.damage -= (int)value;
				break;

			case PowerUpData.Type.Invulnerability: playerData.invulnerabilityDuration -= duration;
				break;

			case PowerUpData.Type.SlowMotion:
				break;
		}
	}
}
