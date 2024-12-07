using System.Collections;
using UnityEngine;

public class CrowdManager : Pausable
{
	[SerializeField] private Transform[] spawnZoneBounds;
	[SerializeField] private float spawnCooldownDuration;
	[SerializeField] private int nbSpawned = 10;
	private bool canSpawn = true;

	private Vector3 RandomPosition
	{
		get
		{
			float x, z;
			x = Random.Range(spawnZoneBounds[0].position.x, spawnZoneBounds[1].position.x);
			z = Random.Range(spawnZoneBounds[0].position.z, spawnZoneBounds[1].position.z);
			return new(x, 0, z);
		}
	}

	private void Update()
	{
		if (paused || !canSpawn) return;
		for (int i = 0; i < nbSpawned; ++i) SpawnCrowdMember();
		canSpawn = false;
		StartCoroutine(SpawnCooldown());
	}

	private void OnDrawGizmos()
	{
		if (spawnZoneBounds.Length == 0) return;

		Gizmos.color = Color.blue;
		foreach (Transform t in spawnZoneBounds) Gizmos.DrawSphere(t.position, 0.5f);
		Vector3[] points = new Vector3[4];
		points[0] = spawnZoneBounds[0].position;
		points[2] = spawnZoneBounds[1].position;
		points[1] = new(points[0].x, points[0].y, points[2].z);
		points[3] = new(points[2].x, points[2].y, points[0].z);
		for (int i = 0; i < points.Length; ++i)
		{
			if(i== points.Length-1) Gizmos.DrawLine(points[i], points[0]);
			else Gizmos.DrawLine(points[i], points[i + 1]);
		}
	}

	private void SpawnCrowdMember()
	{
		PoolManager.Instance.SpawnElement(PoolType.CrowdMember, RandomPosition, Quaternion.identity);
	}

	private IEnumerator SpawnCooldown()
	{
		yield return new WaitForSeconds(spawnCooldownDuration);
		canSpawn = true;
	}
}
