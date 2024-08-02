using System.Collections;
using UnityEngine;

public class OpenCrate : GAction {

    public override bool PrePerform() {
        target = inventory.FindItemWithTag("Crate");
        if (target == null) {
            Debug.Log("No crate found");
            return false;
        }
        GWorld.Instance.RemoveCrate(target);
        Invoke("Timeout", 10f);
        return true;
    }

    public override bool PostPerform() {
        // Add a new state "TreatingPatient"
        beliefs.ModifyState("foundCrate", -1);
        beliefs.ModifyState("openCrate", 1);

        inventory.RemoveItem(target);
        Destroy(target);
        // Hide grass until grown again
        // // Give back the cubicle
        // GWorld.Instance.AddCubicle(target);
        // // Give the cubicle back to the world
        // GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
        // Remove the cubicle from the list
        return true;
    }
}
