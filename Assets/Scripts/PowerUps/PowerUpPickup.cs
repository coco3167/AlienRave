using UnityEngine;

public class PowerUpPickup : Scrolling
{
	[SerializeField] private PowerUpData powerUpData;

	public void OnTriggerEnter(Collider other)
	{
		other.GetComponentInParent<PlayerController>().PickUpPowerUp(powerUpData.PowerUp);
		Despawn();
	}

	protected override void Move()
	{
		rb.linearVelocity = scrollSpeed * Time.deltaTime * Vector3.back;
	}
}
