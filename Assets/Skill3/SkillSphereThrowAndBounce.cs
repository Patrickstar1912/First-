using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class SkillSphereThrowAndBounce : MonoBehaviour
{
   
    [Header("Throw (Parabola)")] public bool throwOnStart = true;

    // Horizontal
    public float horizontalSpeed = 6f;

    // Upward speed (m/s)
    public float upwardSpeed = 5f;

    //  Transform
    public Transform directionSource;

    
    public float yawRandomRange = 15f;

    [Header("Bounce limit")] public string groundTag = "Ground";
    public int maxBounces = 3;

    
    public float stopBounciness = 0.0f;

    
    public float extraDragAfterStop = 1.5f;

    Rigidbody rb;
    Collider col;

    int bounceCount = 0;
    PhysicsMaterial runtimeMat;
    float originalDrag;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        originalDrag = rb.linearDamping; 
        

        runtimeMat = GetComponent<SphereCollider>().material;

        
    }

    void Start()
    {
        if (throwOnStart)
            Throw();
    }

    [ContextMenu("Throw Now")]
    public void Throw()
    {
        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Transform src = directionSource ? directionSource : transform;
        Vector3 forward = src.forward;

        
        if (yawRandomRange > 0.01f)
        {
            float yaw = Random.Range(-yawRandomRange, yawRandomRange);
            forward = Quaternion.Euler(0f, yaw, 0f) * forward;
        }

        forward.y = 0f;
        forward.Normalize();

        
        Vector3 v0 = forward * horizontalSpeed + Vector3.up * upwardSpeed;
        rb.linearVelocity = v0;

        
        rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.VelocityChange);
    }
    
    public void ThrowInternal(Vector3 worldDir)
    {
        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        
        worldDir.y = 0f;
        if (worldDir.sqrMagnitude < 0.0001f)
            worldDir = transform.forward;

        worldDir.Normalize();

       
        if (yawRandomRange > 0.01f)
        {
            float yaw = Random.Range(-yawRandomRange, yawRandomRange);
            worldDir = Quaternion.Euler(0f, yaw, 0f) * worldDir;
        }

        
        Vector3 v0 = worldDir * horizontalSpeed + Vector3.up * upwardSpeed;
        rb.linearVelocity = v0;

        
        rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!string.IsNullOrEmpty(groundTag) && !collision.collider.CompareTag(groundTag))
            return;

        bounceCount++;

        if (bounceCount >= maxBounces)
        {
            
            if (runtimeMat != null)
                runtimeMat.bounciness = stopBounciness;

           
            rb.linearDamping = extraDragAfterStop;
            
            Debug.Log($"Stop bouncing, current bounce count：{bounceCount}");
            Explode();
          
        }
    }
    
    [Header("Explosion Settings")]  
    public float ExplosionRadius = 10f;
    public float ExplosionForce = 500f;  
    public float UpwardsModifier = 2000f;    

    
    void Explode()
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

            if (rb != null && !col.isTrigger)
            {
                rb.AddExplosionForce(ExplosionForce, explosionPos, ExplosionRadius, UpwardsModifier, ForceMode.Impulse );    
            }
        }
        
        Destroy(gameObject, 0.2f);
    }
}