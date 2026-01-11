using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;

public class scrioNPC : MonoBehaviour
{
    [Header("PlayerData")] public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float angularSpeed = 720f;
    public float stoppingDistance = 0f;


    public NavMeshAgent Agent;
    public Rigidbody rb;

    [Header("Patrol location")] public List<Transform> movePosList;
    public Transform CurrMovePos;

    void Start()
    {
        Agent.speed = moveSpeed;
        Agent.acceleration = acceleration;
        Agent.angularSpeed = angularSpeed;
        Agent.stoppingDistance = stoppingDistance;
    }


    void OnMove(Vector3 movePos)
    {
        Agent.SetDestination(movePos);
    }

    //随机选择移动点位
    public void RandomMove()
    {
        var index = Random.Range(0, movePosList.Count);
        CurrMovePos = movePosList[index];
        OnMove(CurrMovePos.position);
    }

    void Update()
    {

        // if (!knocked) return;
        // if (!Agent.enabled) // 说明现在是在被击飞状态
        // {
        //     Debug.Log($"Agent.enabled is false,transform.position.y: {transform.position.y}");
        //     if (transform.position.y <= recoverHeight)
        //     {
        //         RecoverToNavMesh();
        //     }
        // }
    }

    // return;


    // rb.linearVelocity = Vector3.zero;
    //  rb.angularVelocity = Vector3.zero;
    // Agent.Warp(rb.position);

    // public void RecoverAgent()
    // {
    //     Agent.enabled = true;
    // }
    bool knocked;
    [Header("Recover NavMesh Snap")] public float snapRadius = 2.0f;

    public void EnterPhysics()
    {
        knocked = true;

        // 交出控制权：Agent 停止并禁用
        if (Agent.enabled)
        {
            Agent.ResetPath();
            Agent.enabled = false;
        }

        // 物理接管：开启重力，变成非Kinematic
        rb.isKinematic = false;
        rb.useGravity = true;
    }

   public void RecoverToNavMesh()
    {
        // 停止物理
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;

        // 把角色“吸附回 NavMesh”
        if (NavMesh.SamplePosition(transform.position, out var hit, snapRadius, NavMesh.AllAreas))
        {
            if (!Agent.enabled) Agent.enabled = true;
            Agent.Warp(hit.position);
        }
        else
        {
            // 附近没 NavMesh：保持 knocked，或者你可以把它传送到安全点
            Debug.LogWarning("RecoverToNavMesh failed: no NavMesh near current position");
            return;
        }

        knocked = false;
    }

    public float recoverHeight = 2.1f;
}