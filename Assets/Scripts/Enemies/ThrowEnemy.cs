using System.Collections;
using UnityEngine;

public class ThrowEnemy : Enemy
{
	private Transform shootPoint;

	private Vector3 moveDir;
	private int nbProjectileShot;
	private int nbSalvos;

	private ThrowEnemyData Data => data as ThrowEnemyData;

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
		rb.linearVelocity = ((Vector3.back * data.speed) +
							 (moveDir * Data.lateralSpeed)) * Time.deltaTime;
	}

	private void Shoot()
	{
		moveDir = Vector3.zero;
		PoolManager.Instance.SpawnElement(Data.projectileType, shootPoint.position, shootPoint.rotation);
		if (++nbProjectileShot == Data.nbProjPerSalvo) EndSalvo();
		else StartCoroutine(ShootTimer(false));
	}

	private void EndSalvo()
	{
		StartCoroutine(ShootTimer(true));
		nbProjectileShot = 0;

		if (++nbSalvos < Data.nbSalvoBeforeMove) return;

		moveDir = transform.position.x > 0 ? Vector3.left : Vector3.right;
		nbSalvos = 0;
	}

	private IEnumerator ShootTimer(bool restartSalvo)
	{
		yield return new WaitForSeconds(restartSalvo ? Data.salvoCooldown : Data.shootCooldown);
		Shoot();
	}
}
