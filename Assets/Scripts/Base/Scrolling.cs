using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Scrolling : Spawnable
{
	protected Rigidbody rb;
	[SerializeField] protected float scrollSpeed;
	protected bool scrolling = true;

	protected virtual void Awake() => rb = GetComponent<Rigidbody>();

	private void FixedUpdate()
	{
		if (!scrolling || paused) return;
		Move();
	}

	protected virtual void Move()
	{
		rb.linearVelocity = scrollSpeed * Time.deltaTime * Vector3.back;
	}

	public override void Despawn()
	{
		base.Despawn();
		scrolling = false;
	}
	public override void Spawn()
	{
		base.Spawn();
		scrolling = true;
	}
}
