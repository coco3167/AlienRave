using System.Collections;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
	#region Singleton

	private static EnvironmentManager instance;
	public static EnvironmentManager Instance => instance; // TODO faire un vrai singleton
	#endregion

	public bool started;

	private void Awake()
	{
		instance = this;
	}

	[SerializeField] private float respawnTimer;
	private PoolType chunkType = PoolType.EnvironmentChunk;
	public Transform spawnPoint;

	public void SpawnChunk()
	{
		if (!started) return;
		PoolManager.Instance.SpawnElement(PoolType.EnvironmentChunk, spawnPoint.position, spawnPoint.rotation);
	}
}
