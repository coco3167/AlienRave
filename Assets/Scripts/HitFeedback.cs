using UnityEngine;

public class HitFeedback : Spawnable
{
	private ParticleSystem particles;

	private void Awake()
	{
		particles = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if(!particles.isPlaying)
		{
			print($"{name} -> fin du feedback");
			Despawn();
		}
	}

	public override void Spawn()
	{
		base.Spawn();
		particles.Play();
	}
}
