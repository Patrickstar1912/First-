using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class Skill1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float Force = 6f;
    public float DestroyTime = 1f;
    public GameObject Owner;
    public AudioSource skill1audio;
    public AudioClip audioClip;
    public void Start()
    {
        skill1audio.clip = audioClip;  
        skill1audio.Play();
        Destroy(Owner,DestroyTime);    
    }
    void OnTriggerEnter(Collider other)
    {
        
        Rigidbody rb = other.GetComponent<Rigidbody>();
        //if (rb == null) return;

        if (other.GetComponent<scrio>() != null)
        {
            other.GetComponent<scrio>().EnterPhysics();
        }
        else  if (other.GetComponent<scrioNPC>() != null)
        {
            other.GetComponent<scrioNPC>().EnterPhysics();
        }

        Vector3 randomDir = Random.insideUnitSphere;
        randomDir.y = 1f;
        randomDir.Normalize();
        scrio s = other.GetComponent<scrio>();
        
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
