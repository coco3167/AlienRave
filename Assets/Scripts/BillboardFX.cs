using UnityEngine;

public class BillboardFX : MonoBehaviour
{
	Transform cam;
	private void Awake() => cam = Camera.main.transform;
	private void Update() =>
		transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
}
