using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Pausable, IHarmable
{
	[SerializeField] private EntityData data;
	[SerializeField] private float invincibiltyDuration = 0.2f;

	private Vector2 moveInput;
	private bool shooting;
	private Transform shootPoint;
	private float shootTimer;
	private CharacterController ctrl;
	private PlayerInput input;

	private bool canTakeDmg = true;

	private void Awake()
	{
		input = GetComponent<PlayerInput>();
		ctrl = GetComponent<CharacterController>();
		shootPoint = transform.GetChild(0);
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
		if(shooting && shootTimer <= 0)
		{
			PoolManager.Instance.SpawnElement(data.projectileType, shootPoint.position, shootPoint.rotation);
			shootTimer = data.fireRate;
		}
	}

	public void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();

	public void OnShoot(InputAction.CallbackContext ctx) => shooting = ctx.performed;

	protected override void Pause()
	{
		input.enabled = false;
		// TODO Pause anims
	}

	protected override void Play()
	{
		input.enabled = true;
		// TODO Play anims
	}

	public void Harm(int damage)
	{
		if (!canTakeDmg) return;
		print($"{name} poc");
		GameManager.Instance.Harm(damage);
		canTakeDmg = false;
		StartCoroutine(InvincibilityCooldown());
	}

	private IEnumerator InvincibilityCooldown()
	{
		yield return new WaitForSeconds(invincibiltyDuration);
		canTakeDmg = true;
	}
}
