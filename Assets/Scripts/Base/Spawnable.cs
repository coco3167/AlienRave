public abstract class Spawnable : Pausable
{
	public virtual void Spawn() => gameObject.SetActive(true);

	public virtual void Despawn() => gameObject.SetActive(false);
}
