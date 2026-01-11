using System;
using UnityEngine;
using UnityEngine.AI;

public class SkillTornado : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f; // Moving force
    private Rigidbody rb; // Reference RB

    public float liveTime = 5f;
    public AudioSource skill1audio;
    public AudioClip audioClip;
    void Awake()
    {
        skill1audio.clip = audioClip;
        skill1audio.Play();
        // Get RB
        rb = GetComponent<Rigidbody>();

        // If there are no RB add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false; // Adjust gravity
        }

        Destroy(gameObject, liveTime);
    }

    //Apply a force
    public void OnAddForce(Vector3 force)
    {
        rb.AddForce(force * moveForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Role") || other.CompareTag("Player"))
        {
            Debug.Log($"other name: {other.gameObject.name}");
            
            if (other.GetComponent<scrio>() != null)
            {
                other.GetComponent<scrio>().EnterPhysics();
            }
            other.GetComponent<Rigidbody>()?.AddForce(Vector3.up * 300f);
        }
    }
}