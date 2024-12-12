using UnityEngine;

public class EnvironmentChunk : Scrolling
{
	protected override void Awake()
	{
		base.Awake();
		paused = true;
	}

	protected override bool Move()
	{
		if (!base.Move()) return false;
		rb.linearVelocity = Vector3.back * scrollSpeed * Time.deltaTime;
		return true;
	}

	public override void Despawn()
	{
		base.Despawn();
		if(EnvironmentManager.Instance != null) EnvironmentManager.Instance.SpawnChunk();
	}

	public override void Spawn()
	{
		base.Spawn();
		paused = false;
	}
}
