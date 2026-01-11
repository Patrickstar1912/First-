using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillNPC : MonoBehaviour
{
    [Header("Skill1")] public GameObject Skill1Cylinder;

    [Header("Skill2")] public GameObject Meteor;
    public Transform SpawnPosition;
    public float launchForce = 30f;

    [Header("Skill3")] public GameObject skillSpherePrefab;
    public Transform spawnPos;

    [Header("Skill4")] public GameObject skillTornado;
    public Transform tornadoPos;

    void Update()
    {
    }

    public Transform skillTransform;
    private Vector3 skillPos;

    private void Awake()
    {
        skillPos = skillTransform.position;
    }

    //Randomly release skills
    public void RandomSkill()
    {
        int index = Random.Range(1, 5);
        Debug.Log($"skill index is: {index}");
        switch (index)
        {
            case 1:
                CastSkill1();
                break;
            case 2:
                CastSkill2();
                break;
            case 3:
                CastSkill3();
                break;
            case 4:
                CastSkill4();
                break;
        }
    }

    void CastSkill1()
    {
        Vector3 dir = skillPos - transform.position;
        dir.y = 0f;

        Quaternion lookRot = Quaternion.LookRotation(dir);

        Vector3 prefabEuler = Skill1Cylinder.transform.rotation.eulerAngles;
        Quaternion finalRot = Quaternion.Euler(prefabEuler.x, lookRot.eulerAngles.y, lookRot.eulerAngles.z);

        GameObject dick = Instantiate(Skill1Cylinder, skillPos, finalRot);
    }

    void CastSkill2()
    {
        Vector3 targetPos = skillPos;

        GameObject comet = Instantiate(Meteor, SpawnPosition.position, Quaternion.identity);

        Vector3 dir = (targetPos - SpawnPosition.position).normalized;

        Rigidbody rb = comet.GetComponent<Rigidbody>();

        rb.linearVelocity = dir * launchForce;

        comet.transform.rotation = Quaternion.LookRotation(dir);
    }

    void CastSkill3()
    {
        Vector3 dir = skillPos - transform.position;
        dir.y = 0f;
        //
        // Quaternion lookRot = Quaternion.LookRotation(dir);
        //
        // Vector3 prefabEuler = Skill1Cylinder.transform.rotation.eulerAngles;
        // Quaternion finalRot = Quaternion.Euler(prefabEuler.x, lookRot.eulerAngles.y, lookRot.eulerAngles.z);

        var go = Instantiate(skillSpherePrefab, spawnPos.position, Quaternion.identity);
        go.GetComponent<SkillSphereThrowAndBounce>().ThrowInternal(dir);
    }

    void CastSkill4()
    {
        // Calculate the direction from the player to the click location
        Vector3 dir = skillPos - transform.position;
        dir.y = 0f; 
        dir = dir.normalized; 

      
        float fixedSpawnDistance = 5f; 
        Vector3 spawnPosition = transform.position + dir * fixedSpawnDistance;

        var go = Instantiate(skillTornado, spawnPosition, Quaternion.identity);

     
        go.GetComponent<SkillTornado>().OnAddForce(skillPos - transform.position);
    }
}