using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Input Settings")]
    public float InputInterval = 0.8f;
    public List<KeyCode> InputBuffer = new List<KeyCode>();
    public float LastInputTime;

    public Camera Cam;
    public string CurrentSkillName;

    [Header("Skill1")]
    public GameObject Skill1Cylinder;

    [Header("Skill2")]
    public GameObject Meteor;
    public Transform SpawnPosition;
    public float launchForce = 30f;
    
    [Header("Skill3")]
    public GameObject skillSpherePrefab;
    public Transform spawnPos;
    
    [Header("Skill4")]
    public GameObject skillTornado;
    public Transform tornadoPos;
    void Update()
    {
        //InputCountDown
        if (InputBuffer.Count > 0 && Time.time - LastInputTime > InputInterval)
        {
            InputBuffer.Clear();
        }

        if (Input.GetMouseButtonDown(0))
        {
            CastingSkil(CurrentSkillName);
        }

        CheckInput(KeyCode.Q);
        CheckInput(KeyCode.W);
        CheckInput(KeyCode.E);
        CheckInput(KeyCode.R);
    }

    void CheckInput(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            InputBuffer.Add(key);
            LastInputTime = Time.time;

            TryCastSkill();
            
            Debug.Log($"key: {key}");
        }
    }

    void TryCastSkill()
    {
        string combo = GetComboString();

        //Skill Chart
        if (combo == "QE")
        {
            SetSkill1();
            ClearBuffer();
        }
        if (combo == "WR")
        {
            SetSkill2();
            ClearBuffer();
        }
        else if (combo == "QWER")
        {
            SetUltimate();
            ClearBuffer();
        }
        
        if (combo == "QW")
        {
            SetSkill3();
            ClearBuffer();
        }
        if (combo == "ER")
        {
            SetSkill4();
            ClearBuffer();
        }
    }

    string GetComboString()
    {
        string result = "";
        foreach (var key in InputBuffer)
        {
            result += key.ToString();
        }
        return result;
    }

    void ClearBuffer()
    {
        InputBuffer.Clear();
    }

    // Skill Name Setting
    void SetSkill1()
    {
        CurrentSkillName = "CastSkill1";
    }

    void SetSkill2()
    {
        CurrentSkillName = "CastSkill2";
    }
    void SetUltimate()
    {
        CurrentSkillName = "CastUltimate";
    }
    
    void SetSkill3()
    {
        CurrentSkillName = "CastSkill3";
    }
    
    void SetSkill4()
    {
        CurrentSkillName = "CastSkill4";
    }

    public void CastingSkil(string name)
    {
        Debug.Log($"name = {name}");
        Invoke(name, 0f);
        CurrentSkillName = null;
    }

    void CastSkill1()
    {
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0f;

            Quaternion lookRot = Quaternion.LookRotation(dir);

            Vector3 prefabEuler = Skill1Cylinder.transform.rotation.eulerAngles;
            Quaternion finalRot = Quaternion.Euler(prefabEuler.x, lookRot.eulerAngles.y, lookRot.eulerAngles.z);

            GameObject dick = Instantiate(Skill1Cylinder, hit.point, finalRot);
        }

    }

    void CastSkill2()
    {
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Vector3 targetPos = hit.point;

            GameObject comet = Instantiate(Meteor, SpawnPosition.position, Quaternion.identity);

            Vector3 dir = (targetPos - SpawnPosition.position).normalized;

            Rigidbody rb = comet.GetComponent<Rigidbody>();

            rb.linearVelocity = dir * launchForce;

            comet.transform.rotation = Quaternion.LookRotation(dir);

        }

        void CastUltimate()
        {
            Debug.Log("R skill");

        }
    }
    
    void CastSkill3()
    {
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0f;
            //
            // Quaternion lookRot = Quaternion.LookRotation(dir);
            //
            // Vector3 prefabEuler = Skill1Cylinder.transform.rotation.eulerAngles;
            // Quaternion finalRot = Quaternion.Euler(prefabEuler.x, lookRot.eulerAngles.y, lookRot.eulerAngles.z);

            var go = Instantiate(skillSpherePrefab, spawnPos.position, Quaternion.identity);
            go.GetComponent<SkillSphereThrowAndBounce>().ThrowInternal(dir);
        }

    }
    
    void CastSkill4()
    {
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            
            Vector3 dir = hit.point - transform.position;
            dir.y = 0f; 
            dir = dir.normalized; 
            
            
            float fixedSpawnDistance = 5f; 
            Vector3 spawnPosition = transform.position + dir * fixedSpawnDistance;
            
            
            var go = Instantiate(skillTornado, spawnPosition, Quaternion.identity);
            
            
            go.GetComponent<SkillTornado>().OnAddForce(hit.point - transform.position);
        }

    }
}

