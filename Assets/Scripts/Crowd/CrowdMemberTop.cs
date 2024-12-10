using Unity.VisualScripting;
using UnityEngine;

public class CrowdMemberTop : MonoBehaviour
{
	private Rigidbody rb;

	bool fleeing;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		rb.AddForce(-Physics.gravity * Time.deltaTime);
	}
}
