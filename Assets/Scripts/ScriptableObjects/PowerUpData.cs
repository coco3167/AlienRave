using UnityEngine;

public abstract class PowerUpData : ScriptableObject
{
	public float value;

	public enum Type
	{
		Heal, FireRate, NbProjectiles, Homing,
		Speed, Damage, Invulnerability, SlowMotion
	}
	public Type type;

	public abstract PowerUp PowerUp { get; }
}
