using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class HUDManager : MonoBehaviour
{
	private LifeBarManager lifeBar;
	private TextMeshProUGUI scoreTxt;

	[SerializeField] private PowerUpUI[] powerUpUIs;

	private void Awake()
	{
		lifeBar = GetComponentInChildren<LifeBarManager>();
		scoreTxt = GetComponentInChildren<TextMeshProUGUI>();
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

	public void UpdateScore(int score) => scoreTxt.text = $"Score : {score}";
}
