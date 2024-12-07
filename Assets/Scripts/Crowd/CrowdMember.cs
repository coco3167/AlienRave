using System.Collections.Generic;
using UnityEngine;

public class CrowdMember : Scrolling
{
	[SerializeField, Range(0, 10)] private float playerAvoidanceWeight;
	[SerializeField, Range(0, 10)] private float projAvoidanceWeight;
	[SerializeField, Range(0, 10)] private float centerCohesionWeight;
	private readonly List<Transform> playerTransforms = new();
	private readonly List<Transform> projectileTransforms = new();

	Vector3 playerAvoid, projAvoid;

	private void OnDrawGizmos()
	{
		Vector3 origin = transform.position;
		Gizmos.color = Color.red;
		Gizmos.DrawRay(origin, playerAvoid);
		Gizmos.color = Color.green;
		Gizmos.DrawRay (origin, projAvoid);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Contains("Player")) playerTransforms.Add(other.transform);
		else projectileTransforms.Add(other.transform);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag.Contains("Player")) playerTransforms.Remove(other.transform);
		else projectileTransforms.Remove(other.transform);
	}

	protected override void Move()
	{
		if (paused)
		{
			rb.linearVelocity = Vector3.zero;
			return;
		}

		Vector3 scrolling = scrollSpeed * Vector3.back;
		playerAvoid = CalculateAwayVector(playerTransforms) * playerAvoidanceWeight;
		projAvoid = CalculateAwayVector(projectileTransforms) * projAvoidanceWeight;
		rb.linearVelocity = playerAvoid + projAvoid + scrolling * Time.deltaTime;
	}

	private Vector3 CalculateAwayVector(List<Transform> list)
	{
		Vector3 origin = transform.position;
		if (list.Count == 0) return Vector3.zero;
		Vector3 awayVector = Vector3.zero;
		float averageDst = 0;
		foreach (Transform t in list)
		{
			averageDst += Vector3.Distance(origin,t.position);
			awayVector -= t.position - origin;
		}
		averageDst /= list.Count;
		return awayVector * (1 / averageDst);
	}

	public override void Spawn()
	{
		base.Spawn();
		playerTransforms.Clear();
		projectileTransforms.Clear();
	}


}
