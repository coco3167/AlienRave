using UnityEngine;

/// <summary> Script g�rant le comportement d'un membre d'un bloc de foule obstacle. </summary>
public class CrowdObstacle : Spawnable
{
	[HideInInspector] public float speed;
	private Rigidbody rb;

	private void Awake() => rb = GetComponent<Rigidbody>();

	private void Update()
	{
		if (paused)
		{
			rb.linearVelocity = Vector3.zero;
			return;
		}
		rb.linearVelocity = speed * Time.deltaTime * transform.forward;
	}
}
