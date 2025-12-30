using System.Collections.Generic;
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

    [Header("Skill")]
    public GameObject Skill1Cylinder;
    void Update()
    {
        //InputCountDown
        if (InputBuffer.Count > 0 && Time.time - LastInputTime > InputInterval)
        {
            InputBuffer.Clear();
        }

        if(Input.GetMouseButtonDown(0))
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
        else if (combo == "QWER")
        {
            SetUltimate();
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

    // ===== 技能函数 =====
    void SetSkill1()
    {
        CurrentSkillName="CastSkill1";
    }

    void SetUltimate()
    {
        CurrentSkillName = "CastUltimate";
    }

    public void CastingSkil(string name)
    {
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
            Quaternion finalRot = Quaternion.Euler(  prefabEuler.x, lookRot.eulerAngles.y, lookRot.eulerAngles.z );

            GameObject dick = Instantiate(Skill1Cylinder, hit.point, finalRot);
        }

    }

    void CastUltimate()
    {
        Debug.Log("释放 大招！！！");
        
    }
}
