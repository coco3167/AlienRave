using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	#region Singleton

	private static GameManager instance;
	public static GameManager Instance => instance; // TODO faire un vrai singleton
	#endregion

	[SerializeField] private GameObject[] playerPrefabs;

	private PlayerInputManager playerInputManager;
	private int nbPlayers;

	private void Awake()
	{
		instance = this;
		playerInputManager = GetComponent<PlayerInputManager>();
		playerInputManager.playerJoinedEvent.AddListener(OnPlayerJoined);
	}

	public void OnPlayerJoined(PlayerInput playerInput)
	{
		playerInput.onDeviceLost += OnDeviceDisconnected;
		playerInput.onDeviceRegained += OnDeviceReconnected;

		PlayerController ctrl = playerInput.GetComponent<PlayerController>();
		if(nbPlayers++ == 0) playerInputManager.playerPrefab = playerPrefabs[1];
	}

	public void OnDeviceDisconnected(PlayerInput playerInput)
	{

	}

	public void OnDeviceReconnected(PlayerInput playerInput)
	{

	}
}
