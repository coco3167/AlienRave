using UnityEngine;

public abstract class Entity : MonoBehaviour, IPausable
{
	[SerializeField] protected EntityData data;
	protected int health;

	private void Awake() => health = data.maxHealth;

	public virtual void Harm(int damage)
	{
		health -= damage;
		if (health <= 0) Die();
	}

	public abstract void Die();

	public abstract void Pause();

	public abstract void Play();
}
