using System.Collections;
using UnityEngine;

public class EatGrass : GAction {

    public override bool PrePerform() {
        target = inventory.FindItemWithTag("Grass");
        if (target == null) {
            Debug.Log("No grass found");
            return false;
        }
        Invoke("Timeout", 10f);
        return true;
    }

    private void ShowGrass() {
        target.SetActive(true);
        GWorld.Instance.AddGrass(target);
    }

    public override bool PostPerform() {
        beliefs.RemoveState("isHungry");
        beliefs.ModifyState("foundGrass", -1);
        beliefs.ModifyState("eatenGrass", 1);


        inventory.RemoveItem(target);
        target.SetActive(false);
        Invoke("ShowGrass", 10.0f);
        this.GetComponent<GAgent>().Invoke("GetHungry", 10.0f);
        return true;
    }
}
