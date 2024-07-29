using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWater : GAction
{
    public override bool PrePerform()
    {
        // target = GWorld.Instance.GetQueue("waterAvailable").FindResourceClosest(agent.gameObject);
        // if (target == null)
        // {
        //     return false;
        // }
        return true;
    }

    public override bool PostPerform()
    {
        // beliefs.ModifyState("isThirsty", 0);
        return true;
    }
}
