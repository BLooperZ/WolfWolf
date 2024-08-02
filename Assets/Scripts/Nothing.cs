using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Nothin : GAction {

    public override bool PrePerform() {
        Invoke("Timeout", 10f);
        return true;
    }

    public override bool PostPerform() {
        return true;
    }
}
