using System.Collections;
using UnityEngine;

public class ThrowEnemy : Enemy
{
	protected Transform shootPoint;

	protected Vector3 moveDir;
	protected Vector3 scrollDir = Vector3.back;
	protected int nbSalvos;
	protected int nbProjectileShot;

	protected ThrowEnemyData Data => data as ThrowEnemyData;

	protected override void Awake()
	{
		base.Awake();
		shootPoint = transform.GetChild(0);

		//TMP
		StartCoroutine(ShootTimer(true));
	}

	public override void Spawn()
	{
		base.Spawn();
		StartCoroutine(ShootTimer(true));
		
	}

	protected override void Move()
	{
		rb.linearVelocity = ((scrollDir * data.speed) +
							 (moveDir * Data.lateralSpeed)) * Time.deltaTime;
	}

	protected virtual void Shoot()
	{
		moveDir = Vector3.zero;
		PoolManager.Instance.SpawnElement(Data.projectileType, shootPoint.position, shootPoint.rotation);
		AudioManager.Instance.PlayOneShot(FMODEvents.Instance.kisserEnemyKiss, this.transform.position);
		if (++nbProjectileShot == Data.nbProjPerSalvo) EndSalvo();
		else StartCoroutine(ShootTimer(false));
	}

	protected virtual void EndSalvo()
	{
		StartCoroutine(ShootTimer(true));
		nbProjectileShot = 0;

		if (++nbSalvos < Data.nbSalvoBeforeMove)
		{
			scrollDir = Vector3.back;
			return;
		}

		moveDir = transform.position.x > 0 ? Vector3.left : Vector3.right;
		scrollDir = Vector3.zero;
		nbSalvos = 0;
	}

	protected IEnumerator ShootTimer(bool restartSalvo)
	{
		yield return new WaitForSeconds(restartSalvo ? Data.salvoCooldown : Data.shootCooldown);
		Shoot();
	}

	protected override void Die()
	{
		base.Die();
		AudioManager.Instance.PlayOneShot(FMODEvents.Instance.kisserEnemyDeath, this.transform.position);
	}

	public override void Harm(int damage, bool green = false)
	{
		base.Harm(damage, green);
		if (CompareTag("EnemyGreen"))
		{
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.greenKisserEnemyIsHurt, this.transform.position);
		}
		if (CompareTag("EnemyPink"))
		{
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pinkKisserEnemyIsHurt, this.transform.position);
		}
		
	}
}
