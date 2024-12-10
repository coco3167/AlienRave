using UnityEngine;

public class CrowdMemberTop : Scrolling
{
	[SerializeField] private float antiGravityForce = 5;
	private bool fleeing;

	public void ToggleRagdoll(bool on)
	{
		rb.isKinematic = !on;
		fleeing = on;
	}

	protected override void Move()
	{
		if (!fleeing) rb.linearVelocity = Vector3.back;
		else rb.AddForce(antiGravityForce * Time.deltaTime * -Physics.gravity);
	}
}
