public class LastingPowerUp : PowerUp
{
	private readonly float duration;

	public LastingPowerUp(float value, PowerUpData.Type type, float duration) : base(value, type)
	{
		this.duration = duration;
	}

	public override void Apply()
	{
		GameManager gameManager = GameManager.Instance;
		switch (type)
		{
			case PowerUpData.Type.Speed: gameManager.ChangeSpeed(value, duration);
				break;
			case PowerUpData.Type.Damage: gameManager.ChangeDamage((int)value, duration);
				break;
			case PowerUpData.Type.Invulnerability: gameManager.ToggleInvulnerability(duration);
				break;
		}

		gameManager.powerUps.Enqueue(this);
	}

	public void Remove()
	{
		GameManager gameManager = GameManager.Instance;
		switch (type)
		{
			case PowerUpData.Type.Speed: gameManager.ChangeSpeed(-value);
				break;
			case PowerUpData.Type.Damage: gameManager.ChangeDamage(-(int)value);
				break;
			case PowerUpData.Type.Invulnerability: gameManager.ToggleInvulnerability(-1);
				break;
		}
	}
}
