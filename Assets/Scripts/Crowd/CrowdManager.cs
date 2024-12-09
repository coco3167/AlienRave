using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary> Script gérant les différents types de foule présents dans le niveau. </summary>
public class CrowdManager : Pausable
{
	#region Attributs

	#region Paramètres

	/// <summary> Limites de la zone d'apparition de la foule défilant vers les joueurs. </summary>
	[SerializeField] private Transform[] crowdSpawnBounds;
	/// <summary> Limites de la zone d'apparition des blocs de foule "obstacle"
	/// passant latéralements </summary>
	[SerializeField] private Transform[] crowdObstaclesBounds;
	/// <summary> Temps entre chaque spawn d'un membre de la foule défilante. </summary>
	[SerializeField] private float spawnCooldownDuration;
	/// <summary> Nombre de membres apparaissant à chaque interval. </summary>
	[SerializeField] private int nbSpawned = 10;
	#endregion

	#region Foule obstacle

	private int nbObstacleMembers;
	private float obstacleSpeed;
	private Vector3 obstacleOrigin;
	private Vector3 obstacleDirection;
	private bool obstacleSpawning;
	#endregion

	private bool canSpawn = true;
	#endregion

	// TMP
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
		SpawnCrowdObstacles();
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

	/// <summary> Permet de faire apparaître un bloc de foule obstacle au fur et à mesure,
	/// appelé pendant l'update lorsque les paramètres du bloc ont été initialisés. </summary>
	private void SpawnCrowdObstacles()
	{
		if (!obstacleSpawning) return;

		for(int i=0; i<nbObstacleMembers; ++i)
		{
			var obstacle = PoolManager.Instance.SpawnElement(PoolType.CrowdObstacle, obstacleOrigin,
										  Quaternion.LookRotation(obstacleDirection)) as CrowdObstacle;
			obstacle.speed = obstacleSpeed;
		}
	}

	/// <summary> Permet d'initialiser les paramètre d'un bloc de foule obstacle pour lancer son apparition progressive.</summary>
	/// <param name="nb"> Nombre d'élément par interval. </param>
	/// <param name="length"> Durée de l'apparition du bloc, équivalent à la longueur de celui-ci. </param>
	/// <param name="speed"> Vitesse des membres du bloc. </param>
	/// <param name="origin"> Position d'origin des membres à leur apparition. </param>
	/// <param name="direction"> Direction que devront prendre tous les membres du bloc. </param>
	private void InitializeCrowdObstacle(int nb, float length, float speed, Vector3 origin, Vector3 direction)
	{
		obstacleSpawning = true;
		obstacleDirection = direction;
		obstacleOrigin = origin;
		obstacleSpeed = speed;
		nbObstacleMembers = nb;
		StartCoroutine(StopObstacleSpawn(length));
	}

	private IEnumerator SpawnCooldown()
	{
		yield return new WaitForSeconds(spawnCooldownDuration);
		canSpawn = true;
	}

	private IEnumerator StopObstacleSpawn(float duration)
	{
		yield return new WaitForSeconds(duration);
		obstacleSpawning = false;
	}



	//TMP
	public void SpawnRandomObstacle()
	{
		float x, z;
		x = Random.Range(0, 2) == 0 ? crowdObstaclesBounds[0].position.x : crowdObstaclesBounds[1].position.x;
		z = Random.Range(crowdObstaclesBounds[0].position.z, crowdObstaclesBounds[1].position.z);
		Vector3 origin = new(x, 0f, z);
		Vector3 direction = x < 0 ? Vector3.right : Vector3.left;
		direction.z = Random.Range(-0.5f, 0.5f);
		InitializeCrowdObstacle(Random.Range(2, 6), Random.Range(0.2f, 1f), Random.Range(50, 151), origin, direction);
	}
}
