using PathCreation;
using UnityEngine;

public class FollowEnemy : Enemy
{
	private VertexPath path;
	private float dstTravelled;

	protected override void Move()
	{
		if (path == null) return;

		dstTravelled += data.speed * Time.deltaTime;
		Vector3 pos = path.GetPointAtDistance(dstTravelled);
		Quaternion rot = path.GetRotationAtDistance(dstTravelled);
		transform.SetPositionAndRotation(pos, rot);

		if (dstTravelled >= path.length) Despawn();
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
