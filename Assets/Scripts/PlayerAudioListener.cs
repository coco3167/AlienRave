using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioListener : MonoBehaviour
{
	private List<Transform> playerTransforms = new();
	private bool running;

	private void Update()
	{
		if (!running) return;
		transform.position = Vector3.Lerp(playerTransforms[0].position, playerTransforms[1].position, 0.5f);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}

	public void AddPlayer(Transform playerTransform)
	{
		playerTransforms.Add(playerTransform);
		if (playerTransforms.Count == 2) running = true;
	}
}
