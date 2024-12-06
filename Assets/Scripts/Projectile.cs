using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] protected ProjectileData data;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag(data.targetTag)) return;
		Hit(other.GetComponentInParent<Entity>());
	}

	private void Update()
	{
		transform.Translate(transform.forward * data.speed);
	}

	protected virtual void Hit(Entity entity)
	{
		entity.Harm(data.damage);
		gameObject.SetActive(false);
	}
}
