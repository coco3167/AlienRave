using UnityEngine;

public class LevelScrollManager : MonoBehaviour, IPausable
{
	private void Awake()
	{
		GameManager.Instance.OnPause += Pause;
		GameManager.Instance.OnPlay += Play;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Contains("Player")) return;
	}

	public void Pause()
	{
		
	}

	public void Play()
	{
		
	}
}
