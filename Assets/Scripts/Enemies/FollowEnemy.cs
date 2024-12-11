using PathCreation;
using UnityEngine;

public class FollowEnemy : Enemy
{
	private VertexPath path;
	private float dstTravelled;

	protected override bool Move()
	{
		if (paused)
			return false;

		if (path == null) return true;

		dstTravelled += data.speed * Time.deltaTime;
		Vector3 pos = path.GetPointAtDistance(dstTravelled);
		Quaternion rot = path.GetRotationAtDistance(dstTravelled);
		transform.SetPositionAndRotation(pos, rot);

		if (dstTravelled >= path.length) Despawn();

		return true;
	}

	public void StartFollowing(VertexPath path) => this.path = path;

	public override void Despawn()
	{
		gameObject.SetActive(false);
		transform.parent = PoolManager.Instance.transform;
		scrolling = false;
		dstTravelled = 0f;
	}
}
