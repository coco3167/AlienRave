/// <summary> Classe m�re de tous les �l�ments pouvant �tre instanci�s dans la sc�ne. </summary>
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
