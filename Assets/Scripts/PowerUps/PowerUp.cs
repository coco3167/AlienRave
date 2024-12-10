public abstract class PowerUp
{
	public float value;
	public PowerUpData.Type type;

	public PowerUp(float value, PowerUpData.Type type)
	{
		this.value = value;
		this.type = type;
	}

	public abstract void Apply();
}
