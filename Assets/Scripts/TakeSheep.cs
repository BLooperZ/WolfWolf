using System.Collections;
using UnityEngine;

public class TakeSheep : GAction {

    public override bool PrePerform() {
        
        GameObject sheep = inventory.FindItemWithTag("Sheep");

        if (sheep == null) {
            Debug.Log("No crate found");
            return false;
        }
        sheep.GetComponent<Sheep>().beliefs.ModifyState("taken", 1);
        
        beliefs.RemoveState("openCrate");
        sheep.GetComponent<Sheep>().captor = this.gameObject;
        this.GetComponent<Wolf>().chaseSubject = sheep;
        inventory.RemoveItem(sheep);


        // Instantiate an empty GameObject
        GameObject obj = new GameObject();

        obj.transform.position = sheep.transform.position;

        target = obj;

        Invoke("Timeout", 10f);
        return true;
    }

    public override bool PostPerform() {
        // Add a new state "TreatingPatient"
        beliefs.ModifyState("takingSheep", 1);

        // Hide grass until grown again
        // // Give back the cubicle
        // GWorld.Instance.AddCubicle(target);
        // // Give the cubicle back to the world
        // GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
        // Remove the cubicle from the list
        return true;
    }
}
