using UnityEngine;

/// <summary> Classe m�re de tous les �l�ments d�filant � l'�cran. </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class Scrolling : Spawnable
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

	protected abstract void Move();

	public override void Despawn()
	{
		base.Despawn();
		scrolling = false;
		rb.linearVelocity = Vector3.zero;
	}
	public override void Spawn()
	{
		base.Spawn();
		scrolling = true;
	}
}
