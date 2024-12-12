using PathCreation;
using UnityEngine;

public class FollowEnemy : Enemy
{
	[SerializeField] protected string targetTag;
	
	private VertexPath path;
	private float dstTravelled;
	
	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag(targetTag)) return;
		other.transform.GetComponentInParent<IHarmable>().Harm(data.damage);
	}

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
