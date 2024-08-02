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

        GameObject obj = new GameObject();

        obj.transform.position = sheep.transform.position;

        target = obj;

        Invoke("Timeout", 10f);
        return true;
    }

    public override bool PostPerform() {
        beliefs.ModifyState("takingSheep", 1);
        this.GetComponent<Wolf>().indicator.SetActive(false);
        return true;
    }
}
