using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<scrio>().RecoverToNavMesh();
        }
        else if (other.gameObject.CompareTag("Role"))
        {
            other.gameObject.GetComponent<scrioNPC>()?.RecoverToNavMesh();
        }
    }
}