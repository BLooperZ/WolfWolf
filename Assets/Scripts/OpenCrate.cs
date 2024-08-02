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
        beliefs.ModifyState("foundCrate", -1);
        beliefs.ModifyState("openCrate", 1);
        this.GetComponent<Wolf>().indicator.SetActive(true);

        inventory.RemoveItem(target);
        Destroy(target);

        return true;
    }
}
