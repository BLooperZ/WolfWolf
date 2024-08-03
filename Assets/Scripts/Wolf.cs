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
    public GameObject indicator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal("lockSheep", 1, false);
        goals.Add(s1, 5);

        SubGoal s2 = new SubGoal("openCrate", 1, false);
        goals.Add(s2, 3);

        agent = this.GetComponent<NavMeshAgent>();
    }
}
