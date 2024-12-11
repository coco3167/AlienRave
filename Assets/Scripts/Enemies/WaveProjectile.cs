using System.Collections.Generic;
using UnityEngine;

public class WaveProjectile : Spawnable
{
	[SerializeField] private WaveData data;
	private float radius;

	private readonly List<LineRenderer> lines = new();
	private readonly List<Transform> points = new();

	private int Resolution => points.Count;

	private void Awake()
	{
		int i = 0;
		foreach (Transform child in transform)
		{
			points.Add(child);
			var trigger = child.GetComponent<WavePointTrigger>();
			trigger.Initialize(data.damage, data.targetTag);
			int j = i++;
			trigger.OnHit += () => HidePoint(j);
			lines.Add(child.GetComponent<LineRenderer>());
		}
	}

	private void Update()
	{
		radius += data.speed * Time.deltaTime;
		UpdateWave();
		if (radius >= data.maxRadius) Despawn();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, data.minRadius);
	}

	private void HidePoint(int index)
	{
		points[index].gameObject.SetActive(false);
		int previousIndex = index == 0 ? points.Count-1 :  index - 1;
		lines[previousIndex].enabled = false;
	}

	private void UpdateWave()
	{
		var angle = 360 / Resolution;

		Vector3 center = transform.localPosition;
		for (int i = 0; i < Resolution; ++i)
		{
			if (!points[i].gameObject.activeSelf) continue;
			Vector3 pos = Vector3.zero;
			pos.x = (radius * Mathf.Cos((angle * i) * (Mathf.PI / 180)));
			pos.z = (radius * Mathf.Sin((angle * i) * (Mathf.PI / 180)));
			points[i].localPosition = pos;
		}
		for (int i = 0; i < Resolution; ++i)
		{
			if (!points[i].gameObject.activeSelf || !lines[i].enabled) continue;

			Vector3[] pos = new Vector3[2];
			pos[0] = points[i].position;
			pos[1] = i == Resolution - 1 ? points[0].position : points[i + 1].position;
			lines[i].SetPositions(pos);
		}
	}

	public override void Spawn()
	{
		base.Spawn();
		radius = data.minRadius;
		foreach (Transform point in points) point.gameObject.SetActive(true);
		foreach (LineRenderer line in lines) line.enabled = true;
		AudioManager.Instance.PlayOneShot(FMODEvents.Instance.hybridEnemyBurp, transform.position);
	}

	/*private ParticleSystem[] waveParticles;
	private ParticleSystem.MainModule[] waveMain;

	private void Awake()
	{
		waveParticles = GetComponentsInChildren<ParticleSystem>();
		waveMain = new ParticleSystem.MainModule[waveParticles.Length];
		for (int i = 0; i < waveParticles.Length; ++i) waveMain[i] = waveParticles[i].main;
	}

	public void Shoot(Color color)
	{
		for (int i = 0; i < waveParticles.Length; ++i)
		{
			waveMain[i].startColor = color;
			waveParticles[i].Play();
		}
	}*/
}
