public abstract class Spawnable : Pausable
{
	public virtual void Spawn()
	{
		transform.parent = null;
		gameObject.SetActive(true);
	}

	public virtual void Despawn()
	{
		transform.parent = PoolManager.Instance.transform;
		gameObject.SetActive(false);
	}
}
