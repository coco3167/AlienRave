using UnityEngine;

public class Projectile : Spawnable
{
	[SerializeField] protected ProjectileData data;

	private void Update()
	{
		if (paused) return;
		transform.Translate(data.speed * Time.deltaTime * transform.forward, Space.World);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(data.targetTag) || other.CompareTag("Hybrid"))
		{
			Hit(other.GetComponent<IHarmable>());
			PoolManager
			if (other.CompareTag("EnemyGreen"))
			{
				AudioManager.Instance.PlayOneShot(FMODEvents.Instance.greenDrunkEnemyIsHurt, this.transform.position);
			}
			if (CompareTag("EnemyPink"))
			{
				AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pinkDrunkEnemyIsHurt, this.transform.position);
			}
			return;
		}

		if (other.CompareTag("Obstacle")) Despawn();
	}

	protected virtual void Hit(IHarmable entity)
	{
		entity.Harm(data.damage, data.targetTag.Contains("Green"));
		Despawn();
	}
}
