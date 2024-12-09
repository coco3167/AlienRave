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
	[SerializeField] private HUDManager hud;
	[SerializeField] private CrowdManager tmp_crowdManager;
	[SerializeField] private PlayerAudioListener audioListener;

	private PlayerInputManager playerInputManager;
	private int nbPlayers;

	[SerializeField] private int maxPlayersHealth;
	private int playersHealth;

	private float tmpObstacleSPawnertimer = 2f;

	public int score;

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
			tmp_crowdManager.SpawnRandomObstacle();
			tmpObstacleSPawnertimer = 3f;
		}
	}

	public void OnPlayerJoined(PlayerInput playerInput)
	{
		playerInput.onDeviceLost += OnDeviceDisconnected;
		playerInput.onDeviceRegained += OnDeviceReconnected;

		PlayerController ctrl = playerInput.GetComponent<PlayerController>();
		audioListener.AddPlayer(ctrl.transform);
		if (nbPlayers++ == 0) playerInputManager.playerPrefab = playerPrefabs[1];
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

	public void UpdatePowerUps(int playerIndex, Sprite sprite, float timer)
	{
		hud.UpdatePowerUpVisuals(playerIndex, sprite, timer);
		// TODO Anims ?
	}

	public void UpdateScore(int amount)
	{
		score += amount;
		hud.UpdateScore(score);
		// TODO Anims ?
	}

	public void Harm(int damage)
	{
		playersHealth -= damage;

		if (playersHealth <= 0)
		{
			ShowUIScreen(ScreenState.Lose);
			if (playersHealth < 0) playersHealth = 0;
		}

		hud.UpdateLifeVisuals(playersHealth);
	}

	public void Heal(int amount)
	{
		if(playersHealth == maxPlayersHealth) return;

		playersHealth += amount;
		if (playersHealth > maxPlayersHealth) playersHealth = maxPlayersHealth;

		hud.UpdateLifeVisuals(playersHealth);
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
