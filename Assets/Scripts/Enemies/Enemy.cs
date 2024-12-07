using UnityEngine;

public abstract class Enemy : Scrolling, IHarmable
{
	[SerializeField] protected EnemyData data;
	protected int health;
	[SerializeField] protected string targetTag;

	private bool IsDead => health <= 0;

	protected override void Awake()
	{
		base.Awake();
		health = data.maxHealth;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag(targetTag)) return;
		other.transform.GetComponentInParent<IHarmable>().Harm(data.damage);
	}

	public virtual void Harm(int damage)
	{
		if(IsDead) return;
		health -= damage;
		print($"{name} : poc");
		if (health <= 0) Die();
	}

	protected virtual void Die()
	{
		rb.constraints = RigidbodyConstraints.None;
		rb.useGravity = true;
		print($"{name} : ded");
	}

	public override void Despawn()
	{
		base.Despawn();
		// TODO Reset ragdoll
	}

	public void DropPowerUp()
	{
		if (Random.Range(0f, 1f) < data.powerUpDropChance) return;

		// TODO Random avec des poids ?
		int powerUpTypeIndex = Random.Range(9, 16);
		// TODO implémenter le homing et le slowmotion
		if (powerUpTypeIndex == 12) powerUpTypeIndex = 9;
		PoolManager.Instance.SpawnElement((PoolType)powerUpTypeIndex);
	}
}
