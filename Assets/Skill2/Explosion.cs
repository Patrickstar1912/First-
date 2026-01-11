using UnityEngine;
using UnityEngine.AI;

public class Explosion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Explosion Settings")]  
    public float ExplosionRadius = 10f;
    public float ExplosionForce = 800f;  
    public float UpwardsModifier = 2f;    
    public float ExplosionDuration = 0.2f; 
    void Start()
    {
        Vector3 explosionPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, ExplosionRadius);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (col.GetComponent<scrio>() != null)
            {
                col.GetComponent<scrio>().EnterPhysics();
            }
            else  if (col.GetComponent<scrioNPC>() != null)
            {
                col.GetComponent<scrioNPC>().EnterPhysics();
            }

            if (rb != null)
            {
                rb.AddExplosionForce(ExplosionForce, explosionPos, ExplosionRadius, UpwardsModifier, ForceMode.Impulse );    
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, ExplosionDuration); 
    }

}
