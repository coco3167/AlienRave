using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
	[SerializeField] private float shootCooldown = 0.2f;
	[SerializeField] private GameObject tmp_ProjPrefab;
	//[SerializeField] private PoolTag projectileTag;

	private PlayerInput input;
	private Vector2 moveInput;
	private bool shooting;
	private Transform shootPoint;
	private float shootTimer;
	private CharacterController ctrl;

	private void Awake()
	{
		ctrl = GetComponent<CharacterController>();
		input = GetComponent<PlayerInput>();
		shootPoint = transform.GetChild(0);

		shootTimer = shootCooldown;
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
			// TODO récup un projectile et le lancer.
			Instantiate(tmp_ProjPrefab, shootPoint.position, shootPoint.rotation);
			shootTimer = shootCooldown;
		}
	}

	public void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();

	public void OnShoot(InputAction.CallbackContext ctx) => shooting = ctx.performed;
	public override void Die()
	{
		throw new System.NotImplementedException();
	}

	public override void Pause()
	{
		throw new System.NotImplementedException();
	}

	public override void Play()
	{
		throw new System.NotImplementedException();
	}
}
