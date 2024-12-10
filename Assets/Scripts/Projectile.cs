using UnityEngine;

public class Projectile : Spawnable
{
	[SerializeField] protected ProjectileData data;

	private void OnDrawGizmos()
	{
		Gizmos.DrawRay(transform.position, transform.forward * 10);
	}

	private void Update()
	{
		if (paused) return;
		transform.Translate(data.speed * Time.deltaTime * transform.forward, Space.World);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(data.targetTag) || other.CompareTag("Hybrid"))
		{
			Hit(other.GetComponentInParent<IHarmable>());
			return;
		}

		if (other.CompareTag("Obstacle"))
		{
			Despawn();
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.projectileObstacle, this.transform.position);
		}
	}

	protected virtual void Hit(IHarmable entity)
	{
		entity.Harm(data.damage, data.targetTag.Contains("Green"));
		Despawn();
	}
}
