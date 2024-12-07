using System.Collections.Generic;
using UnityEngine;

public class CrowdMember : Scrolling
{
	[SerializeField] private float playerAvoidanceWeight = 1;
	private List<Transform> playerTransforms = new();

	protected override void Move()
	{
		Vector3 scrolling = scrollSpeed * Vector3.back;
		Vector3 playerAvoidance = GetPlayerAvoidanceDirection() * playerAvoidanceWeight;

		rb.linearVelocity = playerAvoidance + scrolling * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.tag.Contains("Player")) return;
		playerTransforms.Add(other.transform);
	}

	private void OnTriggerExit(Collider other)
	{
		if (!other.tag.Contains("Player")) return;
		playerTransforms.Remove(other.transform);
	}

	private Vector3 GetPlayerAvoidanceDirection()
	{
		if(playerTransforms.Count == 0) return Vector3.zero;
		if (playerTransforms.Count == 1) return transform.position - playerTransforms[0].position;
		Vector3 awayVector = Vector3.zero;
		foreach (Transform t in playerTransforms) awayVector -= t.position - transform.position;
		return awayVector;
	}
}
