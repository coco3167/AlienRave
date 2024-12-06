using UnityEngine;

public class Projectile : MonoBehaviour, IPausable
{
	[SerializeField] protected ProjectileData data;

	private bool paused;

	private void Awake()
	{
		GameManager.Instance.OnPause += Pause;
		GameManager.Instance.OnPlay += Play;
	}

	private void Update()
	{
		if (paused) return;
		transform.Translate(transform.forward * data.speed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag(data.targetTag)) return;
		Hit(other.GetComponentInParent<Entity>());
	}

	protected virtual void Hit(Entity entity)
	{
		entity.Harm(data.damage);
		gameObject.SetActive(false);
	}

	public void Pause() => paused = true;
	public void Play() => paused = false;
}
