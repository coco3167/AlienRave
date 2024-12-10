using UnityEngine;

public abstract class PowerUpData : ScriptableObject
{
	public float value;

	public enum Type
	{
		Heal, FireRate, NbProjectiles,
		Speed, Damage, Invulnerability
	}
	public Type type;

	public abstract PowerUp PowerUp { get; }
}
