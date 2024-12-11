using System.Collections;
using UnityEngine;

public abstract class Enemy : Scrolling, IHarmable
{
	[SerializeField] protected EnemyData data;
	[SerializeField] protected float freezeTime;
	protected PoolType powerUpType;
	protected bool hasPowerUp;
	protected Animator anim;
	protected SkinnedMeshRenderer renderer;
	protected int health;

	private bool IsDead => health <= 0;

	protected override void Awake()
	{
		base.Awake();
		anim = GetComponentInChildren<Animator>();
		renderer = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	protected override void Pause()
	{
		base.Pause();
		anim.enabled = false;
	}

	protected override void Play()
	{
		base.Play();
		anim.enabled = true;
	}

	public void AddPowerUp(PoolType powerUpType)
	{
		hasPowerUp = true;
		this.powerUpType = powerUpType;
	}

	public virtual void Harm(int damage, bool green = false)
	{
		if(IsDead) return;
		health -= damage;
		print($"{name} -> poc");

		StartCoroutine(FreezeFrame());
		
		// if (health <= 0) Die();
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
		if (!hasPowerUp) return;
		PoolManager.Instance.SpawnElement(powerUpType, transform.position, Quaternion.identity);
	}

	protected virtual IEnumerator FreezeFrame()
	{ 
		Pause();
		foreach (Material material in renderer.materials)
		{
			material.SetFloat("_IsBlackWhite", 1);
		}
		yield return new WaitForSeconds(freezeTime);
		foreach (Material material in renderer.materials)
		{
			material.SetFloat("_IsBlackWhite", 0);
		}
		
		if (health <= 0) Die();
		
		if(!paused)
			Play();
	}
}
