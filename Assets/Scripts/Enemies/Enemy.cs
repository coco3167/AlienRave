using UnityEngine;

public abstract class Enemy : Scrolling, IHarmable
{
	[SerializeField] protected EntityData data;
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
}
