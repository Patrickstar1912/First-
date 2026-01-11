using System;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    //Speed
    public float moveSpeed = 20f;
    
   
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
            
            if (transform.position == target.position)
            {
                Destroy(gameObject);
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
