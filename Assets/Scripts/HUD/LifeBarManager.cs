using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarManager : MonoBehaviour
{
	[SerializeField] private Sprite[] hpSprites;

	private readonly List<Image> hpImgs = new();

	private void Awake()
	{
		foreach(Transform child in transform) hpImgs.Add(child.GetComponent<Image>());
		UpdateLife(3);
	}

	public void UpdateLife(int health)
	{
		// TODO anims ?
		int i = 1;
		foreach(Image img in hpImgs) img.sprite = hpSprites[i++ > health ? 1 : 0];
	}
}
