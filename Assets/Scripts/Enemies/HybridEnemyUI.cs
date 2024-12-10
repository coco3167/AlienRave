using UnityEngine;
using UnityEngine.UI;

public class HybridEnemyUI : MonoBehaviour
{
	private Slider[] lifeBars;

	private void Awake() => lifeBars = GetComponentsInChildren<Slider>();

	public void Initialize(int maxLife)
	{
		foreach(Slider lifeBar in lifeBars)
		{
			lifeBar.maxValue = maxLife/2;
			lifeBar.value = maxLife/2;
		}
	}

	public void TakeDamage(bool green, int newHeatlh) => lifeBars[green ? 0 : 1].value = newHeatlh;
}
