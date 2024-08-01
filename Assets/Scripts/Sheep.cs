using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sheep : GAgent
{
    public GameObject destination;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        beliefs.ModifyState("isHungry", 1);
        beliefs.ModifyState("isThirsty", 1);

        SubGoal s1 = new SubGoal("eatenGrass", 1, false);
        goals.Add(s1, 1);
        SubGoal s2 = new SubGoal("drankWater", 1, false);
        goals.Add(s2, 1);
        SubGoal s3 = new SubGoal("safe", 1, false);
        goals.Add(s3, 3);

        agent = this.GetComponent<NavMeshAgent>();

        Invoke("GetThirsty", Random.Range(10.0f, 5.0f));
        // Invoke("GetHungry", Random.Range(10.0f, 5.0f));
    }

    void GetThirsty() {

        beliefs.ModifyState("isThirsty", 1);
        //call the get tired method over and over at random times to make the sheep
        //get tired again
        Invoke("GetThirsty", Random.Range(0.0f, 5.0f));
    }

    void GetHungry() {
        Debug.Log("Sheep is hungry");

        beliefs.ModifyState("isHungry", 1);
        beliefs.RemoveState("eatenGrass");
        //call the get tired method over and over at random times to make the sheep
        //get tired again
        // Invoke("GetHungry", Random.Range(0.0f, 5.0f));
    }
}
