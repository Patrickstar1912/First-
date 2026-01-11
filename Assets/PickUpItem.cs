using System;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    //Speed
    public float moveSpeed = 100f;

    public AudioSource pickupaudio;
    public AudioClip audioClip;
    public Transform target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            float arriveDistance = 4f; 

            if (Vector3.Distance(transform.position, target.position) <= arriveDistance)
            
            {
                Debug.Log("shengyin!");
                pickupaudio.clip = audioClip;
                pickupaudio.Play();

                Destroy(gameObject , 3f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(target!=null) return;
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }
}
