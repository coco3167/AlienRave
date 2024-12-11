using UnityEngine;

public class WavePointTrigger : MonoBehaviour
{
	private string targetTag;
	private int damage;

	public delegate void Hit();
	public event Hit OnHit;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Obstacle") || other.tag.Contains("Player"))
		{
			if (other.CompareTag(targetTag)) other.GetComponent<IHarmable>().Harm(damage, targetTag.Contains("Green"));
			OnHit?.Invoke();
			gameObject.SetActive(false);
		}
	}

	public void Initialize(int dmg, string tag)
	{
		damage = dmg;
		targetTag = tag;
	}
}
