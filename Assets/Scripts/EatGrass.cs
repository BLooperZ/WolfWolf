using System.Collections;
using UnityEngine;

public class EatGrass : GAction {

    public override bool PrePerform() {
        target = inventory.FindItemWithTag("Grass");
        if (target == null) {
            Debug.Log("No grass found");
            return false;
        }
        return true;
    }

    public override bool PostPerform() {
        // Add a new state "TreatingPatient"
        beliefs.RemoveState("isHungry");
        beliefs.ModifyState("foundGrass", -1);
        beliefs.ModifyState("eatenGrass", 1);


        inventory.RemoveItem(target);
        Destroy(target);
        // Hide grass until grown again
        // // Give back the cubicle
        // GWorld.Instance.AddCubicle(target);
        // // Give the cubicle back to the world
        // GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
        // Remove the cubicle from the list
        this.GetComponent<GAgent>().Invoke("GetHungry", 10.0f);
        return true;
    }
}
