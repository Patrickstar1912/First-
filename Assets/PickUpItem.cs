using System;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    //移动速度
    public float moveSpeed = 5f;
    
    //移动目标
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
            //匀速向目标移动
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            //到达目标点后销毁
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
