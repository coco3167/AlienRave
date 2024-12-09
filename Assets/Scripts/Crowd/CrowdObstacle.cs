using UnityEngine;

public class CrowdObstacle : Spawnable
{
	[HideInInspector] public float speed;
	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (paused) return;
		rb.linearVelocity = speed * Time.deltaTime * transform.forward;
	}
}
