using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Singleton

	private static GameManager instance;
	public static GameManager Instance => instance; // TODO faire un vrai singleton
	#endregion

	private enum ScreenState { Start, Pause, Win, Lose, Disconnected }
	[SerializeField] private PlayerData[] playerDatas;
	[SerializeField] private GameObject[] playerPrefabs;
	[SerializeField] private HUDManager hud;
	[SerializeField] private CrowdManager tmp_crowdManager;
	[SerializeField] private PlayerAudioListener audioListener;
	[SerializeField] private List<GameObject> menus;
	[SerializeField] private bool needsForTwoPlayers;

	private PlayerInputManager playerInputManager;
	private int nbPlayers;
	
	private static bool restart;

	[SerializeField] private int maxPlayersHealth;
	private int playersHealth;

	//private float tmpObstacleSPawnertimer = 2f;

	public bool areUWinningSon = true;
	public int score;

	#region �v�nements

	public delegate void TimeChange();
	public event TimeChange OnPause;
	public event TimeChange OnPlay;
	#endregion

	private void Awake()
	{
		instance = this;
		playerInputManager = GetComponent<PlayerInputManager>();
		playerInputManager.playerJoinedEvent.AddListener(OnPlayerJoined);
		playerInputManager.playerJoinedEvent.AddListener(menus[0].GetComponent<UI.StartMenu>().OnPlayerJoined);
		foreach (var data in playerDatas) data.ResetData();
		playersHealth = maxPlayersHealth;

		if (restart)
		{
			restart = false;
			return;
		}

		SetStartMenu();
	}

	private void Update()
	{
		// tmpObstacleSPawnertimer -= Time.deltaTime;
		// if (tmpObstacleSPawnertimer <= 0)
		// {
		// 	tmp_crowdManager.TMP_SpawnRandomObstacle();
		// 	tmpObstacleSPawnertimer = 3f;
		// }
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
		// TODO Bloquer l'input jusqu'� la reconnection.
	}

	public void OnDeviceReconnected(PlayerInput playerInput)
	{
		// TODO R�activer l'input.
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
		if (playersHealth <= 0) ShowUIScreen(ScreenState.Lose);
		else hud.UpdateLifeVisuals(playersHealth);
	}

	public void Heal(int amount)
	{
		if(playersHealth == maxPlayersHealth) return;

		playersHealth += amount;
		if (playersHealth > maxPlayersHealth) playersHealth = maxPlayersHealth;

		hud.UpdateLifeVisuals(playersHealth);
	}

	public void SetStartMenu()
	{
		ShowUIScreen(ScreenState.Start);
		OnPause?.Invoke();
	}

	public void Pause()
	{
		ShowUIScreen(ScreenState.Pause);
		OnPause?.Invoke();
	}

	public bool Play()
	{
		if (needsForTwoPlayers && playerInputManager.playerCount < 2)
			return false;
		
		OnPlay?.Invoke();
		HideUIScreen();
		return true;
	}

	public void Restart(bool showMainMenu)
	{
		restart = !showMainMenu;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void WinLevel()
	{
		ShowUIScreen(ScreenState.Win);
	}

	private void ShowUIScreen(ScreenState state)
	{
		switch(state)
		{
			case ScreenState.Start:
				menus[0].SetActive(true);
				break;
			case ScreenState.Pause:
				menus[1].SetActive(true);
				break;
			case ScreenState.Win:
				areUWinningSon = true;
				OnPause?.Invoke();
				menus[2].SetActive(true);
				break;
			case ScreenState.Lose:
				areUWinningSon = false;
				OnPause?.Invoke();
				menus[2].SetActive(true);
				break;
		}
	}

	public void HideUIScreen()
	{
		menus.ForEach(x => x.SetActive(false));
	}
}
