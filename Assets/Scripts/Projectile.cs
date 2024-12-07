using UnityEngine;

public class Projectile : Spawnable
{
	[SerializeField] protected ProjectileData data;

	private void Update()
	{
		if (paused) return;
		transform.Translate(transform.forward * data.speed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(data.targetTag)) Hit(other.GetComponentInParent<IHarmable>());
		Despawn();
	}

	protected virtual void Hit(IHarmable entity)
	{
		entity.Harm(data.damage);
		gameObject.SetActive(false);
	}
}
