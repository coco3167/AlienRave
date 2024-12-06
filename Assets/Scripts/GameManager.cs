using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton

	private static GameManager instance;
	public static GameManager Instance => instance; // TODO faire un vrai singleton
	#endregion

	private void Awake()
	{
		instance = this;
	}


}
