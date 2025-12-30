using UnityEngine;

public class Skill1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float Force = 8f;
    public float DestroyTime = 0.5f;
    public GameObject Owner;
    public void Start()
    {
        Destroy(Owner,DestroyTime);    
    }
    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 randomDir = Random.insideUnitSphere;
        randomDir.y = 1f;
        randomDir.Normalize();

        rb.AddForce(randomDir * Force, ForceMode.Impulse);
    }
    
}
