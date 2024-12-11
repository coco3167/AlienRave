using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
	private ParticleSystem[] uiParticles;

	private void Awake() =>
		uiParticles = GetComponentsInChildren<ParticleSystem>();

	public void Toggle(bool on)
	{
		foreach (ParticleSystem p in uiParticles)
		{
			if (on) p.Play();
			else p.Stop();
		}
	}
}
