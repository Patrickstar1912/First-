using UnityEngine;
using UnityEngine.AI;


public class scrio : MonoBehaviour
{
    [Header("PlayerData")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float angularSpeed = 720f;
    public float stoppingDistance = 0f;

    [Header("Arrow")]
    public GameObject Arrow;
    public float ClickInterval;
    public float TargetClickInterval;

    public Camera Cam;

    public NavMeshAgent Agent;
    void Start()
    {
        Agent.speed = moveSpeed;
        Agent.acceleration = acceleration;
        Agent.angularSpeed = angularSpeed; 
        Agent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        ClickInterval += Time.unscaledDeltaTime;

        if(ClickInterval>= TargetClickInterval)
        {
           ClickInterval = TargetClickInterval;
        }

        if (Input.GetMouseButtonDown(1)&& ClickInterval>=TargetClickInterval)
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            ClickInterval = 0;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject arrowObj = Instantiate(Arrow, hit.point + Vector3.up * 0.1f, Quaternion.identity);
                Destroy(arrowObj, 0.5f);
                Agent.SetDestination(hit.point);
            }
        }
    }
}