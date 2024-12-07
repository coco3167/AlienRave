using UnityEngine;

public class DrunkEnemy : Enemy
{
	[SerializeField, Range(0,1)] private float maxAngle;

	private Vector3 walkDirection;

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

	protected override void Move()
	{
		rb.linearVelocity = data.speed * Time.deltaTime * walkDirection;
	}
}
