using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
	private Image powerUpImg;
	private Image timerImg;

	private float timer;
	private float maxTimer;

	private void Awake()
	{
		powerUpImg = transform.GetChild(0).GetComponent<Image>();
		powerUpImg.enabled = false;
		timerImg = transform.GetChild(1).GetComponent<Image>();
		timerImg.enabled = false;
	}

	private void Update()
	{
		if(timer > 0)
		{
			timer -= Time.deltaTime;
			timerImg.fillAmount = timer / maxTimer;
			if (timer <= 0)
			{
				powerUpImg.enabled = false;
				timerImg.enabled = false;
			}
		}
	}

	public void UpdatePowerUpDisplay(Sprite sprite, float timer)
	{
		powerUpImg.sprite = sprite;
		powerUpImg.enabled = true;
		timerImg.enabled = true;

		this.timer = timer;
		maxTimer = timer;
	}
}
