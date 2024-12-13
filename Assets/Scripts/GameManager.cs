using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
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
	[SerializeField] private GameObject[] playerPrefabs;
	[SerializeField] private PlayerData[] playerDatas;

	[SerializeField] private HUDManager hud;
	[SerializeField] private CrowdManager tmp_crowdManager;
	[SerializeField] private PlayerAudioListener audioListener;
	[SerializeField] private List<GameObject> menus;
	[SerializeField] private bool needsForTwoPlayers;
	[SerializeField] private StartMenu startMenu;
	[SerializeField] private Transform[] playerSpawnPos;
	[SerializeField] private Animator playerHurtAnim;

	private PlayerInputManager playerInputManager;
	private PlayerController[] players = new PlayerController[2];
	private int nbPlayers;
	
	private static bool restart;
	private bool isPlaying;

	private LDTool.LevelAnimationSpawner.MusicState musicState;

	[SerializeField] private int maxPlayersHealth;
	private int playersHealth;
	private bool isDead;

	//private float tmpObstacleSPawnertimer = 2f;

	public bool areUWinningSon = true;
	public int score;
	private int multi = 1;
	private float multiLastingTime = .5f;
	private Coroutine multiResetCoroutine;

	[HideInInspector] public Queue<LastingPowerUp> powerUps = new();

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
		playerInputManager.playerJoinedEvent.AddListener(menus[0].GetComponent<UI.StartMenu>().OnPlayerJoined);
		foreach (var data in playerDatas) data.ResetData();
		playersHealth = maxPlayersHealth;
	}

	private void Start()
	{
		if (restart)
		{
			restart = false;
			return;
		}

		SetStartMenu();
		AudioManager.Instance.SetMusicParameter("LevelState", "StartMenu");
	}

	public void OnPlayerJoined(PlayerInput playerInput)
	{
		playerInput.onDeviceLost += OnDeviceDisconnected;
		playerInput.onDeviceRegained += OnDeviceReconnected;

		PlayerController ctrl = playerInput.GetComponent<PlayerController>();
		ctrl.transform.position = playerSpawnPos[nbPlayers].position;
		players[nbPlayers] = ctrl;
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

	public void UpdatePowerUps(PowerUp powerUp, float timer = 0)
	{
		switch(powerUp.type)
		{
			case PowerUpData.Type.FireRate:
			case PowerUpData.Type.NbProjectiles: return;

			case PowerUpData.Type.Heal: timer = 2f;
				break;
		}

		hud.UpdatePowerUpVisuals(powerUp.type, timer);
	}

	public void UpdateScore(int amount)
	{
		score += amount*multi;
		multi++;
		AudioManager.Instance.PlayOneShot(FMODEvents.Instance.scoreUp, this.transform.position);
		if (!multiResetCoroutine.IsUnityNull())
			StopCoroutine(multiResetCoroutine);
		multiResetCoroutine = StartCoroutine(ResetMulti());
		hud.UpdateScore(score);
		hud.UpdateMulti(multi);
		// TODO Anims ?
	}

	private IEnumerator ResetMulti()
	{
		float time = 0f;
		while (time < multiLastingTime)
		{
			time = Mathf.Clamp(time+.01f, 0, multiLastingTime);
			
			hud.UpdateMultiSlider(time/multiLastingTime);
			yield return new WaitForSeconds(.01f);
		}
		multi = 1;
		hud.UpdateMulti(multi);
	}

	public int GetFinalScore()
	{
		return (playersHealth+1) * score;
	}

	public void Harm(int damage)
	{
		playersHealth -= damage;
		playerHurtAnim.SetTrigger("Hurt");
		if (!isDead && playersHealth <= 0)
		{
			isDead = true;
			ShowUIScreen(ScreenState.Lose);
		}
		else hud.UpdateLifeVisuals(playersHealth);
	}

	#region Power Ups

	public void Heal(int amount)
	{
		if (playersHealth == maxPlayersHealth) return;

		playersHealth += amount;
		if (playersHealth > maxPlayersHealth) playersHealth = maxPlayersHealth;

		hud.UpdateLifeVisuals(playersHealth);
		hud.UpdatePowerUpVisuals(PowerUpData.Type.Heal, 2f);
	}

	public void IncreaseFireRate(float amount)
	{
		if (playerDatas[0].fireRate == 0.01f) return;

		foreach (var data in playerDatas) data.fireRate -= amount;

		if (playerDatas[0].fireRate <= 0.01f)
			foreach (var data in playerDatas) data.fireRate = 0.01f;
	}

	public void IncreaseNbProjectiles(int amount)
	{
		if (playerDatas[0].nbProjectiles == 4) return;
		foreach (var player in players) player.IncreaseShootPoints();
	}

	public void ChangeSpeed(float amount, float duration = -1)
	{
		foreach (var data in playerDatas) data.speed += amount;

		if (duration > 0)
		{
			foreach (var player in players) player.ToggleSpeedFeedback();
			StartCoroutine(PowerUpTimer(duration));
		}
		hud.UpdatePowerUpVisuals(PowerUpData.Type.Speed, duration);
	}

	public void ChangeDamage(int amount, float duration = -1)
	{
		foreach (var data in playerDatas) data.projectileData.damage += amount;

		foreach (var player in players) player.ToggleFeedback(true, duration > 0);

		if (duration > 0) StartCoroutine(PowerUpTimer(duration));
	}

	public void ToggleInvulnerability(float duration)
	{
		foreach (var player in players) player.ToggleFeedback(false, duration > 0);

		if (duration < 0)
		{
			foreach (var data in playerDatas) data.invulnerabilityDuration = 0.2f;
			return;
		}

		StartCoroutine(PowerUpTimer(duration));
		hud.UpdatePowerUpVisuals(PowerUpData.Type.Invulnerability, duration);

		for (int i=0; i<players.Length; ++i)
		{
			playerDatas[i].invulnerabilityDuration = duration;
			players[i].Invulnerability();
		}
	}

	public IEnumerator PowerUpTimer(float duration)
	{
		yield return new WaitForSeconds(duration);
		LastingPowerUp powerUp = powerUps.Dequeue();
		powerUp.Remove();
	}
	#endregion

	public void SetStartMenu()
	{
		AudioManager.Instance.SetMusicParameter("GameStatus", "Play");
		ShowUIScreen(ScreenState.Start);
		OnPause?.Invoke();
	}

	public void Pause()
	{
		if(!isPlaying)
			return;

		AudioManager.Instance.SetMusicParameter("GameStatus", "Pause");
		isPlaying = false;
		ShowUIScreen(ScreenState.Pause);
		OnPause?.Invoke();
	}

	public bool Play()
	{
		if (needsForTwoPlayers && playerInputManager.playerCount < 2)
			return false;

		AudioManager.Instance.SetMusicParameter("GameStatus", "Play");

		isPlaying = true;
		OnPlay?.Invoke();
		HideUIScreen();
		EnvironmentManager.Instance.started = true;
		return true;
	}

	/*public void EndTuto()
	{
		print("non");
		foreach (var player in players) player.tuto = false;
		tuto = false;
		Play();
	}*/

	public void OnPlayerInstantiated(bool green)
	{
		if (green && needsForTwoPlayers) return;
		startMenu.GetFocus();
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
				StartCoroutine(TransitionStateScreen(1));
				break;
			case ScreenState.Win:
				areUWinningSon = true;
				OnPause?.Invoke();
				StartCoroutine(TransitionStateScreen(2));
				AudioManager.Instance.SetMusicParameter("LevelState", "Success");
				break;
			case ScreenState.Lose:
				areUWinningSon = false;
				OnPause?.Invoke();
				StartCoroutine(TransitionStateScreen(2));
				AudioManager.Instance.SetMusicParameter("LevelState", "GameOver");
				break;
		}
	}

	// ReSharper disable Unity.PerformanceAnalysis
	private IEnumerator TransitionStateScreen(int index, bool appearing = true)
	{
		CanvasGroup group = menus[index].GetComponent<CanvasGroup>();
		
		group.alpha = appearing ? 0 : 1;
		if(appearing)
			menus[index].SetActive(true);

		float difference = (appearing ? 1 : -1) * .08f;
		
		while (appearing ? group.alpha < 1 : group.alpha > 0)
		{
			group.alpha = Mathf.Clamp01(group.alpha + difference);
			yield return new WaitForSeconds(.001f);
		}
		
		if(!appearing)
			menus[index].SetActive(false);
	}

	public void HideUIScreen()
	{
		for (int loop = 0; loop < menus.Count; loop++)
		{
			if (menus[loop].activeSelf)
				StartCoroutine(TransitionStateScreen(loop, false));
		}
		// menus.ForEach(x => x.SetActive(false));
	}

	public void ChangeMainMusicState(LDTool.LevelAnimationSpawner.MusicState newMusicState)
	{
		musicState = newMusicState;
		Debug.Log(musicState);
		switch (newMusicState)
		{
			case LDTool.LevelAnimationSpawner.MusicState.Chapter2:
				AudioManager.Instance.SetMusicParameter("LevelState", "Chapter 2");
				break;
			case LDTool.LevelAnimationSpawner.MusicState.Chapter3:
				AudioManager.Instance.SetMusicParameter("LevelState", "Chapter 3");
				break;
		}
		AudioManager.Instance.SetMusicParameter("LevelState", "musicState");
	}
}
