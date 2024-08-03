using System.Collections;
using UnityEngine;

public class Follow : GAction {

    public override bool PrePerform() {
        target = this.GetComponent<Sheep>().captor.GetComponent<Wolf>().pen;
        if (target == null) {
            Debug.Log("No captor found");
            return false;
        }
        return true;
    }

    public override bool PostPerform() {
        return true;
    }
}
