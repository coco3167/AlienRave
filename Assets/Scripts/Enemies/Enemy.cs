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
		health = data.maxHealth;
		anim = GetComponentInChildren<Animator>();
	}

	public virtual void Harm(int damage, bool green = false)
	{
		if(IsDead) return;
		health -= damage;
		print($"{name} : poc");
		if (health <= 0) Die();
	}

	protected virtual void Die()
	{
		ToggleRagdoll(true);
		print($"{name} : ded");
		DropPowerUp();
	}

	public override void Despawn()
	{
		base.Despawn();
		anim.enabled = true;
		ToggleRagdoll(false);
	}

	public virtual void ToggleRagdoll(bool on)
	{
		rb.constraints = on ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotation;
		rb.useGravity = on;
		if(anim != null) anim.enabled = !on;
	}

	public void DropPowerUp()
	{
		if (Random.Range(0f, 1f) < data.powerUpDropChance) return;

		// TODO Random avec des poids ?
		int powerUpTypeIndex = Random.Range(9, 16);
		// TODO implémenter le homing et le slowmotion
		if (powerUpTypeIndex == 12) powerUpTypeIndex = 9;
		PoolManager.Instance.SpawnElement((PoolType)powerUpTypeIndex,transform.position, Quaternion.identity);
	}
}
