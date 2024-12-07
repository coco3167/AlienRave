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
	[SerializeField] private CrowdManager tmp_crowdManager;

	private PlayerInputManager playerInputManager;
	private int nbPlayers;

	[SerializeField] private int maxPlayersHealth;
	private int playersHealth;

	private float tmpObstacleSPawnertimer = 2f;

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

	private void Update()
	{
		tmpObstacleSPawnertimer -= Time.deltaTime;
		if (tmpObstacleSPawnertimer <= 0)
		{
			tmp_crowdManager.TMP_SpawnRandomObstacle();
			tmpObstacleSPawnertimer = 3f;
		}
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

	public void UpdatePowerUps(Sprite sprite, int playerIndex)
	{
		// TODO Afficher le sprite du powerup du côté du joueur.
	}

	public void Harm(int damage)
	{
		playersHealth -= damage;
		if (playersHealth <= 0) ShowUIScreen(ScreenState.Lose);
		// TODO Update UI.
	}
	public void Heal(int amount)
	{
		if(playersHealth == maxPlayersHealth) return;

		playersHealth += amount;
		if (playersHealth > maxPlayersHealth) playersHealth = maxPlayersHealth;
		// TODO Update UI.
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
