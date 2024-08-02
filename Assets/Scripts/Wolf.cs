using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : GAgent
{
    public GameObject destination;
    public GameObject pen;

    private NavMeshAgent agent;

    public GameObject chaseSubject;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // beliefs.ModifyState("isHungry", 1);
        // beliefs.ModifyState("isThirsty", 1);

        SubGoal s1 = new SubGoal("lockSheep", 1, false);
        goals.Add(s1, 5);

        SubGoal s2 = new SubGoal("openCrate", 1, false);
        goals.Add(s2, 3);

        // SubGoal s2 = new SubGoal("drankWater", 1, false);
        // goals.Add(s2, 1);
        // SubGoal s3 = new SubGoal("safe", 1, false);
        // goals.Add(s3, 3);

        agent = this.GetComponent<NavMeshAgent>();
        // Invoke("GetHungry", Random.Range(10.0f, 5.0f));
    }
}
