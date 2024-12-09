using System.Collections.Generic;
using UnityEngine;

/// <summary> Script gérant le comportement d'un membre de la foule défilante. </summary>
public class CrowdMember : Scrolling
{
	#region Attributs

	#region Paramètres

	[Tooltip("Poids de la force appliquée pour éviter les joueurs")]
	[SerializeField, Range(0, 10)] private float playerAvoidanceWeight;
	[Tooltip("Poids de la force appliquée pour éviter les projectiles des joueurs")]
	[SerializeField, Range(0, 10)] private float projAvoidanceWeight;
	[Tooltip("Poids de la force appliquée pour rester au centre du niveau")]
	[SerializeField, Range(0, 10)] private float centerCohesionWeight;
	#endregion

	private readonly List<Transform> playerTransforms = new();
	private readonly List<Transform> projectileTransforms = new();

	// Uniquement visuel pour le OnDrawGizmos.
	Vector3 playerAvoid, projAvoid;
	#endregion

	private void OnDrawGizmos()
	{
		Vector3 origin = transform.position;
		Gizmos.color = Color.red;
		Gizmos.DrawRay(origin, playerAvoid);
		Gizmos.color = Color.green;
		Gizmos.DrawRay (origin, projAvoid);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Contains("Player")) playerTransforms.Add(other.transform);
		else projectileTransforms.Add(other.transform);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag.Contains("Player")) playerTransforms.Remove(other.transform);
		else projectileTransforms.Remove(other.transform);
	}

	protected override void Move()
	{
		if (paused)
		{
			rb.linearVelocity = Vector3.zero;
			return;
		}

		Vector3 scrolling = scrollSpeed * Vector3.back;
		playerAvoid = CalculateAwayVector(playerTransforms) * playerAvoidanceWeight;
		projAvoid = CalculateAwayVector(projectileTransforms) * projAvoidanceWeight;
		rb.linearVelocity = playerAvoid + projAvoid + scrolling * Time.deltaTime;
	}

	/// <summary> Permet de calculer la direction d'évitement en fonction d'une liste d'objets à éviter. </summary>
	/// <param name="list"> Les objets à éviter. </param>
	/// <returns> La direction d'évitement la plus directe. </returns>
	private Vector3 CalculateAwayVector(List<Transform> list)
	{
		Vector3 origin = transform.position;
		if (list.Count == 0) return Vector3.zero;

		Vector3 awayVector = Vector3.zero;
		float averageDst = 0;
		foreach (Transform t in list)
		{
			averageDst += Vector3.Distance(origin,t.position);
			awayVector -= t.position - origin;
		}
		averageDst /= list.Count;

		// Ajout d'un poids inversement proportionnel à la distance moyenne.
		return awayVector * (1 / averageDst);
	}

	public override void Spawn()
	{
		base.Spawn();
		playerTransforms.Clear();
		projectileTransforms.Clear();
	}
}
