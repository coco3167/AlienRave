using System.Collections;
using UnityEngine;

public abstract class Enemy : Scrolling, IHarmable
{
	[SerializeField] protected EnemyData data;
	protected float freezeTime = .2f;
	protected PoolType powerUpType;
	protected bool hasPowerUp;
	protected SkinnedMeshRenderer rend;
	protected Animator anim;
	protected int health;

	private bool IsDead => health <= 0;
	protected int pauseStack;

	protected override void Awake()
	{
		base.Awake();
		rend = GetComponentInChildren<SkinnedMeshRenderer>();
		anim = GetComponentInChildren<Animator>();
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
	}

	protected virtual IEnumerator FreezeFrame()
	{
		Pause();
		
		foreach (Material material in rend.materials)
		{
			material.SetFloat("_Effect", 1f);
		}

		yield return new WaitForSeconds(freezeTime);
		
		foreach (Material material in rend.materials)
		{
			material.SetFloat("_Effect", 0f);
		}
		
		if (health <= 0) Die();
		
		Play();
	}

	protected override void Pause()
	{
		pauseStack++;
		if (pauseStack < 1)
			return;
		base.Pause();
		anim.enabled = false;
	}

	protected override void Play()
	{
		pauseStack--;
		if(pauseStack > 0)
			return;
		base.Play();
		anim.enabled = true;
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
}
