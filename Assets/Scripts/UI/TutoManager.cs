using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutoManager : MonoBehaviour
{
	#region Singleton

	private static TutoManager instance;
	public static TutoManager Instance => instance; // TODO faire un vrai singleton
	#endregion
	[SerializeField] private GameObject tutoScreen;
	[SerializeField] private VideoPlayer videoPlayer;
	[HideInInspector] public bool inTuto;
	bool canSkip;
	bool menuContext;

	private void Awake()
	{
		instance = this;
	}

	private void Update()
	{
		if(inTuto && canSkip && !videoPlayer.isPlaying) EndTuto();
	}

	public void LaunchTuto(bool menuContext = false)
	{
		print(menuContext);
		this.menuContext = menuContext;
		canSkip = false;
		inTuto = true;

		tutoScreen.SetActive(true);
		videoPlayer.Play();
		StartCoroutine(TimeBeforeSkip());
	}

	public void Skip()
	{
		if (!canSkip) return;
		EndTuto();
	}

	private void EndTuto()
	{
		videoPlayer.Stop();
		tutoScreen.SetActive(false);
		inTuto = false;
		GameManager.Instance.hasHadTuto = true;
		if (!menuContext) GameManager.Instance.Play();
		else GameManager.Instance.ReshowPause();
	}

	private IEnumerator TimeBeforeSkip()
	{
		yield return new WaitForSeconds(2f);
		canSkip = true;
	}
}
