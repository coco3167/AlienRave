using UnityEngine;

public abstract class Enemy : Scrolling, IHarmable
{
	[SerializeField] protected EntityData data;
	protected int health;

	protected override void Awake()
	{
		base.Awake();
		health = data.maxHealth;
	}

	public virtual void Harm(int damage)
	{
		health -= damage;
		if (health <= 0) Die();
	}

	private void Die()
	{
		// TODO Ragdoll
	}
}
