using UnityEngine;
using UnityEngine.AI;
using System.Collections;
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
        //if (rb == null) return;

        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();

        Vector3 randomDir = Random.insideUnitSphere;
        randomDir.y = 1f;
        randomDir.Normalize();
        scrio s = other.GetComponent<scrio>();
        
        if (agent != null)
        {
            agent.enabled = false;


          
        }
        
        rb.AddForce(randomDir * Force, ForceMode.Impulse);
    }
    IEnumerator RecoverAgent(NavMeshAgent agent, Rigidbody rb, Vector3 dir)
    {
        
        rb.isKinematic = false;

        rb.AddForce(dir * Force, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);

        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        agent.Warp(rb.position);
        agent.enabled = true;
    }
}
