using UnityEngine;

public abstract class Pausable : MonoBehaviour
{
	protected bool paused;

	protected virtual void Start()
	{
		GameManager.Instance.OnPause += Pause;
		GameManager.Instance.OnPlay += Play;
	}

	protected virtual void Pause() => paused = true;

	protected virtual void Play() => paused = false;
}
