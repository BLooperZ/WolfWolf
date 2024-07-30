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
        SubGoal s1 = new SubGoal("isThirsty", 0, false);
        goals.Add(s1, 3);
        SubGoal s2 = new SubGoal("isHungry", 0, false);
        goals.Add(s2, 3);

        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(destination.transform.position);
    }
}
