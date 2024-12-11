using UnityEngine;

/// <summary> Classe m�re de tous les �l�ments sur lesquels
/// il est n�cessaire d'agir lors de la mise en pause du jeu. </summary>
public abstract class Pausable : MonoBehaviour
{
	protected bool paused;

	protected virtual void Start()
	{
		GameManager.Instance.OnPause += Pause;
		GameManager.Instance.OnPlay += Play;
	}

	protected virtual void Pause()
	{
		paused = true;
	}

	protected virtual void Play()
	{
		paused = false;
	}
}
