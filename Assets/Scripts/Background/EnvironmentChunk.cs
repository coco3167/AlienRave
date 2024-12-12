using UnityEngine;

public class EnvironmentChunk : Scrolling
{
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
}
