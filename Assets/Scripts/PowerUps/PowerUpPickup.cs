using UnityEngine;

public class PowerUpPickup : Scrolling
{
	[SerializeField] private PowerUpData powerUpData;

	public void OnTriggerEnter(Collider other)
	{
		other.GetComponentInParent<PlayerController>().PickUpPowerUp(powerUpData.PowerUp);
		AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pickUp, this.transform.position);
		Despawn();
	}

	protected override bool Move()
	{
		if (!base.Move())
			return false;
		
		rb.linearVelocity = scrollSpeed * Time.deltaTime * Vector3.back;
		
		return true;
	}
}
