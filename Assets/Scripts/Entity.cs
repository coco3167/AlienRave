using UnityEngine;

public abstract class Entity : MonoBehaviour, IPausable
{
	[SerializeField] protected EntityData data;
	protected int health;

	protected virtual void Awake()
	{
		GameManager.Instance.OnPause += Pause;
		GameManager.Instance.OnPlay += Play;
		health = data.maxHealth;
	}

	public virtual void Harm(int damage)
	{
		health -= damage;
		if (health <= 0) Die();
	}

	public abstract void Die();

	public abstract void Pause();

	public abstract void Play();
}
