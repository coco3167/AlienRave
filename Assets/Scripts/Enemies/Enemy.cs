using System;
using UnityEngine;

public abstract class Enemy : Scrolling, IHarmable
{
	[SerializeField] protected EnemyData data;
	protected Animator anim;
	protected int health;

	private bool IsDead => health <= 0;

	protected override void Awake()
	{
		base.Awake();
		anim = GetComponentInChildren<Animator>();
	}

	public void AddPowerUp(PoolType powerUpType) => data.powerUpType = powerUpType;

	public virtual void Harm(int damage, bool green = false)
	{
		if(IsDead) return;
		health -= damage;
		if (health <= 0) Die();
	}

	protected virtual void Die()
	{
		//ToggleRagdoll(true);
		GameManager.Instance.UpdateScore(data.scoreValue);
		DropPowerUp();
		Despawn();
	}

	protected virtual void ResetLife() => health = data.maxHealth;

	public override void Despawn()
	{
		base.Despawn();
		ToggleRagdoll(false);
	}
	public override void Spawn()
	{
		base.Spawn();
		ResetLife();
	}

	public virtual void ToggleRagdoll(bool on)
	{
		rb.constraints = on ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotation;
		rb.useGravity = on;
		if(anim != null) anim.enabled = !on;
	}

	public void DropPowerUp()
	{
		if (!data.hasPowerUp) return;
		PoolManager.Instance.SpawnElement(data.powerUpType, transform.position, Quaternion.identity);
	}
}
