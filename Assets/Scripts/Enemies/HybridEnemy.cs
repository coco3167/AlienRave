using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridEnemy : ThrowEnemy
{
	private PoolType RandomProjectileType => Random.Range(0, 2) == 0 ? 
												PoolType.GreenWaveProjectile : 
												PoolType.PinkWaveProjectile;

	private Color RandomColor => Random.Range(0, 2) == 0 ? new Color(1, 0, 1) : new Color(0, 1, 0);

	protected override void Shoot()
	{
		moveDir = Vector3.zero;
		scrollDir = Vector3.zero;
		PoolManager.Instance.SpawnElement(RandomProjectileType, shootPoint.position, Quaternion.identity);

		if (++nbProjectileShot == Data.nbProjPerSalvo) StartCoroutine(SalvoEndDelay());
		else StartCoroutine(ShootTimer(false));
	}

	private IEnumerator SalvoEndDelay()
	{
		yield return new WaitForSeconds(Data.salvoCooldown);
		EndSalvo();
	}
}
