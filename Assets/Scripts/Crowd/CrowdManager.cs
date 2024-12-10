using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary> Script g�rant les diff�rents types de foule pr�sents dans le niveau. </summary>
public class CrowdManager : Pausable
{
	#region Attributs

	#region Param�tres

	/// <summary> Limites de la zone d'apparition de la foule d�filant vers les joueurs. </summary>
	[SerializeField] private Transform[] crowdSpawnBounds;
	/// <summary> Limites de la zone d'apparition des blocs de foule "obstacle"
	/// passant lat�ralements </summary>
	[SerializeField] private Transform[] crowdObstaclesBounds;
	/// <summary> Temps entre chaque spawn d'un membre de la foule d�filante. </summary>
	[SerializeField] private float spawnCooldownDuration;
	/// <summary> Nombre de membres apparaissant � chaque interval. </summary>
	[SerializeField] private int nbSpawned = 10;
	#endregion
	#endregion

	private bool canSpawn = true;

	private Vector3 CrowdSpawnPosition
	{
		get
		{
			float x, z;
			x = Random.Range(crowdSpawnBounds[0].position.x, crowdSpawnBounds[1].position.x);
			z = Random.Range(crowdSpawnBounds[0].position.z, crowdSpawnBounds[1].position.z);
			return new(x, 0, z);
		}
	}

	private void Update()
	{
		if (paused) return;
		SpawnCrowdMembers();
		//SpawnCrowdObstacles();
	}

	private void OnDrawGizmos()
	{
		if (crowdSpawnBounds.Length > 0)
		{
			Vector3[] points = new Vector3[4];
			points[0] = crowdSpawnBounds[0].position;
			points[2] = crowdSpawnBounds[1].position;
			points[1] = new(points[0].x, points[0].y, points[2].z);
			points[3] = new(points[2].x, points[2].y, points[0].z);
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(points[0], 0.5f);
			Gizmos.DrawSphere(points[2], 0.5f);
			for (int i = 0; i < points.Length; ++i)
			{
				if (i == points.Length - 1) Gizmos.DrawLine(points[i], points[0]);
				else Gizmos.DrawLine(points[i], points[i + 1]);
				Gizmos.DrawSphere(points[i], 0.5f);
			}
		}
		
		if (crowdObstaclesBounds.Length > 0)
		{
			Vector3[] points = new Vector3[4];
			points[0] = crowdObstaclesBounds[0].position;
			points[2] = crowdObstaclesBounds[1].position;
			points[1] = new(points[0].x, points[0].y, points[2].z);
			points[3] = new(points[2].x, points[2].y, points[0].z);
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(points[0], 0.5f);
			Gizmos.DrawSphere(points[2], 0.5f);
			for (int i = 0; i < points.Length; ++i)
			{
				if (i == points.Length - 1) Gizmos.DrawLine(points[i], points[0]);
				else Gizmos.DrawLine(points[i], points[i + 1]);
				Gizmos.DrawSphere(points[i], 0.5f);
			}
		}
	}

	private void SpawnCrowdMembers()
	{
		if (!canSpawn) return;
		for (int i = 0; i < nbSpawned; ++i)
			PoolManager.Instance.SpawnElement(PoolType.CrowdMember, CrowdSpawnPosition, Quaternion.identity);

		canSpawn = false;
		StartCoroutine(SpawnCooldown());
	}

	/// <summary> Permet de faire appara�tre un bloc de foule obstacle au fur et � mesure,
	/// appel� pendant l'update lorsque les param�tres du bloc ont �t� initialis�s. </summary>
	private IEnumerator SpawnCrowdObstacles(int length, Vector3 direction, float zSpread, float xPos, float zPos, float zBasePos, float zLength, float speed, int nb)
	{
		for (int loop = 0; loop < length; ++loop)
		{
			for (int i = 0; i < nb; ++i)
			{
				float zPosModif = zPos + Random.Range(-zSpread, zSpread); // gets pos noise, maybe <-1 or >1
				zPosModif -= (float)Math.Truncate(zPosModif)*(zPosModif % 1); // repartition of <-1 and >1 back between -1 and 1
				float zRealPos = Mathf.Lerp(zBasePos, zLength, zPosModif);
				Vector3 obstaclePos = new Vector3(xPos, 0.01f, zRealPos);
				
				var obstacle = PoolManager.Instance.SpawnElement(PoolType.CrowdObstacle, obstaclePos,
					Quaternion.LookRotation(direction)) as CrowdObstacle;
				obstacle.speed = speed;
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForSeconds(0.5f);
		}
	}

	public void SpawnCrowdObstacles(CrowdData crowdData, float xPos, float zPos, float zBasePos, float zLength, Vector3 direction)
	{
		StartCoroutine(SpawnCrowdObstacles(crowdData.length, direction, crowdData.zSpread, xPos, zPos, zBasePos, zLength, crowdData.speed, crowdData.nb));
	}

	private IEnumerator SpawnCooldown()
	{
		yield return new WaitForSeconds(spawnCooldownDuration);
		canSpawn = true;
	}
}
