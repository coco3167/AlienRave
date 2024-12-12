using UnityEngine;
using TMPro;
using System.Collections;

public class HUDManager : MonoBehaviour
{
	private LifeBarManager lifeBar;
	[SerializeField] private TextMeshProUGUI scoreTxt;
	[SerializeField] private TextMeshProUGUI multiTxt;
	[SerializeField] private Transform multiSlider;
    [SerializeField] private ProgressBar progressBar;

	[SerializeField] private PowerUpUI[] powerUpUIs;

	private void Awake()
	{
		lifeBar = GetComponentInChildren<LifeBarManager>();
	}

	public void UpdateLifeVisuals(int health) => lifeBar.UpdateLife(health);

	public void UpdatePowerUpVisuals(PowerUpData.Type type, float timer)
	{
		int vfxIndex = type switch
		{
			PowerUpData.Type.Heal => 0,
			PowerUpData.Type.Invulnerability => 1,
			_ => 2,
		};

		powerUpUIs[vfxIndex].Toggle(true);
		StartCoroutine(ClearPowerUpVisuals(vfxIndex, timer));
	}

	private IEnumerator ClearPowerUpVisuals(int index, float timer)
	{
		yield return new WaitForSeconds(timer);
		powerUpUIs[index].Toggle(false);
	}

	public void InitializeProgressBar() => progressBar.SetMaxValue(60 * 3);
	public void PauseProgressBar() => progressBar.Pause();
	public void RunProgressBar() => progressBar.Run();
	public void UpdateScore(int score) => scoreTxt.text = $"Score : {score}";

	public void UpdateMulti(int multi) => multiTxt.text = $"X{multi}";

	public void UpdateMultiSlider(float fillPortion) => multiSlider.localScale = new Vector3(1-fillPortion, 1, 1);
}
