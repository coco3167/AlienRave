using UnityEngine;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour
{
	#region Singleton

	private static TutoManager instance;
	public static TutoManager Instance => instance; // TODO faire un vrai singleton
	#endregion

	[SerializeField] private Sprite[] phoneSprites;
	[SerializeField] private Image phoneImg;
	private int imgIndex;

	private enum AwaitPlayerInputType { Green, Pink, Both }
	private AwaitPlayerInputType awaitPlayer = AwaitPlayerInputType.Green;

	private void Awake()
	{
		instance = this;
	}

	public void NextImage(bool green)
	{
		print("green ?" + green);
		print(awaitPlayer);
		if (awaitPlayer != AwaitPlayerInputType.Both &&
		  ((awaitPlayer == AwaitPlayerInputType.Green && !green) || (awaitPlayer == AwaitPlayerInputType.Pink && green)))
			return;

		if(++imgIndex == 5)
		{
			//GameManager.Instance.EndTuto();
			gameObject.SetActive(false);
			return;
		}

		phoneImg.sprite = phoneSprites[imgIndex];
		if (imgIndex == 4) awaitPlayer = AwaitPlayerInputType.Both;
		else awaitPlayer = imgIndex % 2 == 0 ? AwaitPlayerInputType.Green : AwaitPlayerInputType.Pink;
	}
}
