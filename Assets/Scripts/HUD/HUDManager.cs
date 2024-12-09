using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
	private LifeBarManager lifeBar;
	private TextMeshProUGUI scoreTxt;

	private readonly List<PowerUpUI> powerUpUIs = new();

	private void Awake()
	{
		lifeBar = GetComponentInChildren<LifeBarManager>();
		scoreTxt = GetComponentInChildren<TextMeshProUGUI>();

		Transform powerUpUisParent = transform.GetChild(2);
		foreach (Transform child in powerUpUisParent) 
			powerUpUIs.Add(child.GetComponent<PowerUpUI>());
	}

	public void UpdateLifeVisuals(int health) => lifeBar.UpdateLife(health);

	public void UpdatePowerUpVisuals(int playerIndex, Sprite sprite, float timer)
	{
		powerUpUIs[playerIndex].UpdatePowerUpDisplay(sprite, timer);
	}

	public void UpdateScore(int score) => scoreTxt.text = $"Score : {score}";
}
