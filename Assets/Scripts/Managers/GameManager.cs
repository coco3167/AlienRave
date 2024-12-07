using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	#region Singleton

	private static GameManager instance;
	public static GameManager Instance => instance; // TODO faire un vrai singleton
	#endregion

	private enum ScreenState { Pause, Win, Lose, Disconnected }

	[SerializeField] private GameObject[] playerPrefabs;

	private PlayerInputManager playerInputManager;
	private int nbPlayers;

	[SerializeField] private int maxPlayersHealth;
	private int playersHealth;

	#region Événements

	public delegate void TimeChange();
	public event TimeChange OnPause;
	public event TimeChange OnPlay;
	#endregion

	private void Awake()
	{
		instance = this;
		playerInputManager = GetComponent<PlayerInputManager>();
		playerInputManager.playerJoinedEvent.AddListener(OnPlayerJoined);

		playersHealth = maxPlayersHealth;
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
		ShowUIScreen(ScreenState.Disconnected);
		// TODO Bloquer l'input jusqu'à la reconnection.
	}

	public void OnDeviceReconnected(PlayerInput playerInput)
	{
		// TODO Réactiver l'input.
	}

	public void Harm(int damage)
	{
		playersHealth -= damage;
		if (playersHealth <= 0) ShowUIScreen(ScreenState.Lose);
	}

	public void Pause()
	{
		ShowUIScreen(ScreenState.Pause);
		OnPause?.Invoke();
	}

	public void Play()
	{
		OnPlay?.Invoke();
		HideUIScreen();
	}

	private void ShowUIScreen(ScreenState state)
	{
		switch(state)
		{
			case ScreenState.Pause:
				break;
			case ScreenState.Win:
				break;
			case ScreenState.Lose:
				break;
		}
	}

	public void HideUIScreen()
	{
	
	}
}
