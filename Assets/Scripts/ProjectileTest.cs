using UnityEngine;

public class ProjectileTest : Spawnable
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnSpawn()
    {
        float xForce = Random.Range(-100f, 100f);
        float yForce = Random.Range(50f, 100f);
        float zForce = Random.Range(-100f, 100f);
        rb.AddForce(xForce, yForce, zForce);
    }

    public override void Despawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
