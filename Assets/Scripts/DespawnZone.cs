using UnityEngine;

public class DespawnZone : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		other.GetComponentInParent<Spawnable>().Despawn();
	}
}
