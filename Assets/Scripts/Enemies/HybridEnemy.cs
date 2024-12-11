using System.Collections;
using UnityEngine;

public class HybridEnemy : ThrowEnemy
{
	private HybridEnemyUI ui;
	private int greenHealth;
	private int pinkHealth;

	private PoolType RandomProjectileType => Random.Range(0, 2) == 0 ?
												PoolType.GreenWaveProjectile :
												PoolType.PinkWaveProjectile;

	private Color RandomColor => Random.Range(0, 2) == 0 ? new Color(1, 0, 1) : new Color(0, 1, 0);

	protected override void Awake()
	{
		base.Awake();
		ui = GetComponentInChildren<HybridEnemyUI>();
	}

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

	public override void Harm(int damage, bool green = false)
	{
		print($"Hybrid poc -> green : {green} -> {damage}");
		if ((green && greenHealth == 0) || (!green && pinkHealth == 0)) return;

		if (green)
		{
			greenHealth -= damage;
			if (greenHealth <= 0) greenHealth = 0;
			ui.TakeDamage(true, greenHealth);
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.greenHybridEnemyIsHurt, this.transform.position);
		}
		else
		{
			pinkHealth -= damage;
			if (pinkHealth <= 0) pinkHealth = 0;
			ui.TakeDamage(false, pinkHealth);
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pinkHybridEnemyIsHurt, this.transform.position);
		}

		if (pinkHealth == 0 && greenHealth == 0)
		{
			Die();
			AudioManager.Instance.PlayOneShot(FMODEvents.Instance.hybridEnemyDeath, this.transform.position);
		}
	}

	protected override void ResetLife()
	{
		greenHealth = data.maxHealth / 2;
		pinkHealth = data.maxHealth / 2;
		ui.Initialize(data.maxHealth);
	}
}
