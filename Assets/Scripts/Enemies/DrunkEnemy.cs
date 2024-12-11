using UnityEngine;
public class DrunkEnemy : Enemy
{
	[SerializeField, Range(0,1)] private float maxAngle;
	[SerializeField] protected string targetTag;
	private Vector3 walkDirection;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag(targetTag)) return;
		other.transform.GetComponentInParent<IHarmable>().Harm(data.damage);
	}

	protected override void Awake()
	{
		base.Awake();
		walkDirection = Vector3.back;
		walkDirection.x = Random.Range(-maxAngle, maxAngle);
	}

	public override void Spawn()
	{
		base.Spawn();
		walkDirection = Vector3.back;
		walkDirection.x = Random.Range(-maxAngle, maxAngle);
		
		if (CompareTag("EnemyGreen"))
		{
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.greenDrunkEnemySeesPlayer, this.transform.position);
		}

		if (CompareTag("EnemyPink"))
		{
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pinkDrunkEnemySeesPlayer, this.transform.position);
		}
	}

	protected override bool Move()
	{
		if (!base.Move())
			return false;
		
		rb.linearVelocity = data.speed * Time.deltaTime * walkDirection;
		return true;
	}

	public override void Harm(int damage, bool green = false)
	{
		base.Harm(damage, green);
		if (CompareTag("EnemyGreen"))
		{
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.greenDrunkEnemyIsHurt, this.transform.position);
		}
		if (CompareTag("EnemyPink"))
		{
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pinkDrunkEnemyIsHurt, this.transform.position);
		}
	}

	protected override void Die()
	{
		base.Die();
	}
}
