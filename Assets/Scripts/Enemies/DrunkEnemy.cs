using UnityEngine;

public class DrunkEnemy : Enemy
{
	[SerializeField, Range(0,1)] private float maxAngle;
	[SerializeField] protected string targetTag;
	private Vector3 walkDirection;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag(targetTag)) return;
		other.transform.GetComponentInParent<IHarmable>().Harm(data.damage);
	}

	protected override void Awake()
	{
		base.Awake();
		walkDirection = Vector3.back;
		walkDirection.x = Random.Range(-maxAngle, maxAngle);
	}

	public override void Spawn()
	{
		base.Spawn();
		walkDirection = Vector3.back;
		walkDirection.x = Random.Range(-maxAngle, maxAngle);
	}

	protected override bool Move()
	{
		if (!base.Move())
			return false;
		
		rb.linearVelocity = data.speed * Time.deltaTime * walkDirection;
		return true;
	}
	protected override void Die()
	{
		base.Die();
	}
}
