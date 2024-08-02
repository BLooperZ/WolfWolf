using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sheep : GAgent
{
    public GameObject destination;

    public GameObject captor;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        beliefs.ModifyState("isHungry", 1);
        beliefs.ModifyState("isThirsty", 1);

        SubGoal s1 = new SubGoal("eatenGrass", 1, false);
        goals.Add(s1, 1);
        SubGoal s2 = new SubGoal("safe", 1, false);
        goals.Add(s2, 3);

        SubGoal s3 = new SubGoal("home", 1, false);
        goals.Add(s3, 4);

        agent = this.GetComponent<NavMeshAgent>();

    }

    void GetHungry() {
        Debug.Log("Sheep is hungry");

        beliefs.ModifyState("isHungry", 1);
        beliefs.RemoveState("eatenGrass");
    }
}
