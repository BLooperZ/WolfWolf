using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : GAgent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("isThirsty", 0, false);
        goals.Add(s1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
