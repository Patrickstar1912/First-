using System;
using UnityEngine;

public class NpcBrain : MonoBehaviour
{
    //State enumeration
    public enum BrainState
    {
        Move,
        SKill
    }
    //State switching time
    public float stateChangeTime = 3.0f;

    private float currTime;
    public scrioNPC NpcMove;
    public SkillNPC NpcSkill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime > stateChangeTime)
        {
            currTime = 0;
         
            int index = UnityEngine.Random.Range(0, 2);
            ChangeState(index==0?BrainState.Move:BrainState.SKill);
        }
    }

    //Switch status
    public void ChangeState(BrainState state)
    {
        switch (state)
        {
            case BrainState.Move:
                Debug.Log("Npc Move");
                NpcMove.RandomMove();
                break;
            case BrainState.SKill:
                Debug.Log("Npc Skill");
                NpcSkill.RandomSkill();
                break;
            default:
                break;
        }
    }
}

