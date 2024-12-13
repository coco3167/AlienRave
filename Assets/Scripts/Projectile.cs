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
			Feedback(true, other.transform);
			Hit(other.GetComponent<IHarmable>());
			if (other.CompareTag("EnemyGreen"))
			{
				AudioManager.Instance.PlayOneShot(FMODEvents.Instance.greenDrunkEnemyIsHurt, this.transform.position);
			}
			if (other.CompareTag("EnemyPink"))
			{
				AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pinkDrunkEnemyIsHurt, this.transform.position);
			}
			return;
		}
		if (data.targetTag == "EnemyPink" && other.CompareTag("EnemyGreen")
		    || data.targetTag == "EnemyGreen" && other.CompareTag("EnemyPink")
		    || data.targetTag == "PlayerGreen" && other.CompareTag("PlayerPink")
		    || data.targetTag == "PlayerPink" && other.CompareTag("PlayerGreen")
		    || other.CompareTag("Obstacle"))
		{
			Feedback(false, other.transform);
			Despawn();
		}
	}

	protected virtual void Hit(IHarmable entity)
	{
		entity.Harm(data.damage, data.targetTag.Contains("Green"));
		Despawn();
	}

	public void Feedback(bool hit, Transform receiver)
	{
		PoolType feedbackType;
		if (hit)
		{
			feedbackType = data.targetTag.Contains("Green") ?
					PoolType.FeedbackHitGreen : PoolType.FeedbackHitPink;
		}
		else
		{
			feedbackType = data.targetTag.Contains("Green") ?
					PoolType.FeedbackNoHitGreen : PoolType.FeedbackNoHitPink;
		}

		var feedbackTransform = PoolManager.Instance.SpawnElement(feedbackType, transform.position, transform.rotation).transform;
		feedbackTransform.parent = receiver;
	}
}
