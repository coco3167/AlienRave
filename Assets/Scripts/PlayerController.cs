using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Pausable, IHarmable
{
	[SerializeField] private PlayerData data;

	private Vector2 moveInput;
	private bool shooting;
	private Transform[] shootPoints;
	private float shootTimer;
	private CharacterController ctrl;
	private Animator anim;
	private PlayerInput input;
	private float powerUpTimer;
	private bool pausing;

	private bool canTakeDmg = true;

	private void Awake()
	{
		input = GetComponent<PlayerInput>();
		ctrl = GetComponent<CharacterController>();
		anim = GetComponentInChildren<Animator>();
		shootPoints = new Transform[] { transform.GetChild(0).GetChild(0) };
		shootTimer = data.fireRate;
	}

	private void FixedUpdate()
	{
		//Vector2 moveInput = controls.Movement.Direction.ReadValue<Vector2>();
		Vector3 velocity = data.speed * new Vector3(moveInput.x, 0, moveInput.y);
		Vector3 motion = transform.right * velocity.x + transform.forward * velocity.z;
		ctrl.Move(motion * Time.fixedDeltaTime);
	}

	private void Update()
	{
		if(shootTimer > 0) shootTimer -= Time.deltaTime;
		if (shooting && shootTimer <= 0) Shoot();
		if(pausing) SetPause();
	}

	public void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();

	public void OnShoot(InputAction.CallbackContext ctx)
	{
		shooting = ctx.performed;
		anim.SetBool("Shoot", shooting);
	}
	
	public void OnPause(InputAction.CallbackContext ctx) => pausing = ctx.performed;
	
	public void IncreaseShootPoints()
	{
		if (data.nbProjectiles == 4) return;
		Transform shootParent = transform.GetChild(0);

		shootPoints = ++data.nbProjectiles switch
		{
			2 => new Transform[] { shootParent.GetChild(1), shootParent.GetChild(2) },
			3 => new Transform[]
			{
				shootParent.GetChild(0),
				shootParent.GetChild(3),
				shootParent.GetChild(4),
			},
			_ => new Transform[]
			{
				shootParent.GetChild(5),
				shootParent.GetChild(6),
				shootParent.GetChild(7),
				shootParent.GetChild(8),
			}
		};
	}

	public void Shoot()
	{
		foreach(var shootPoint in shootPoints)
			PoolManager.Instance.SpawnElement(data.projType, shootPoint.position, shootPoint.rotation);

		
		shootTimer = data.fireRate;
	}

	private void SetPause()
	{
		pausing = false;
		GameManager.Instance.Pause();
	}

	protected override void Pause()
	{
		// input.enabled = false;
		// TODO Pause anims
	}

	protected override void Play()
	{
		input.enabled = true;
		// TODO Play anims
	}

	public void Harm(int damage, bool green = false)
	{
		if (!canTakeDmg) return;
		print($"{name} poc");
		anim.SetTrigger("Hurt");
		GameManager.Instance.Harm(damage);
		Invulnerability();
	}

	public void PickUpPowerUp(PowerUp powerUp)
	{
		Sprite sprite = powerUp.Apply(data, this);
		if(sprite != null)
		{
			// Si le powerUp touche les deux joueur playerIndex = 2;
			int index = powerUp.type == PowerUpData.Type.SlowMotion ? 2 : data.playerIndex;
			GameManager.Instance.UpdatePowerUps(index, sprite, powerUpTimer);
		}
	}

	public void Invulnerability()
	{
		canTakeDmg = false;
		StartCoroutine(InvulnerabilityCooldown());
		// TODO Anims / effets visuels ?
	}

	public IEnumerator PowerUpTimer(float duration)
	{
		powerUpTimer = duration;
		yield return new WaitForSeconds(duration);
		LastingPowerUp powerUp = data.powerUps.Dequeue();
		powerUp.Remove();
	}

	private IEnumerator InvulnerabilityCooldown()
	{
		yield return new WaitForSeconds(data.invulnerabilityDuration);
		canTakeDmg = true;
	}
}
