using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPen : GAction
{
    public override bool PrePerform()
    {
        target = this.GetComponent<Wolf>().pen;

        Invoke("Timeout", 10f);
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.RemoveState("takingSheep");
        this.GetComponent<Wolf>().chaseSubject.GetComponent<Sheep>().captor = null;
        this.GetComponent<Wolf>().chaseSubject.GetComponent<Sheep>().beliefs.RemoveState("taken");
        this.GetComponent<Wolf>().chaseSubject = null;
        return true;
    }
}
