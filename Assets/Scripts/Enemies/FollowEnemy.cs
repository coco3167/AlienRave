using PathCreation;
using UnityEngine;

public class FollowEnemy : Enemy
{
	public PathCreator PathCreator { private get; set; }
	private float dstTravelled;

	protected override void Move()
	{
		dstTravelled += data.speed * Time.deltaTime;
		Vector3 pos = PathCreator.path.GetPointAtDistance(dstTravelled);
		Quaternion rot = PathCreator.path.GetRotationAtDistance(dstTravelled);
		transform.SetPositionAndRotation(pos, rot);
		if (dstTravelled >= 1) Despawn();
	}

	public override void Despawn()
	{
		base.Despawn();
		dstTravelled = 0f;
	}
}
