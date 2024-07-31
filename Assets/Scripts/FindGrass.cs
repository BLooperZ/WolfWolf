﻿public class FindGrass : GAction {

    public string actionName = "FindGrass";

    public override bool PrePerform() {
        target = GWorld.Instance.RemoveGrass();
        if (target == null)
            return false;
        inventory.AddItem(target);
        return true;
    }

    public override bool PostPerform() {

        // Add a new state "TreatingPatient"
        beliefs.ModifyState("eatenGrass", 1);
        // Hide grass until grown again
        // // Give back the cubicle
        // GWorld.Instance.AddCubicle(target);
        // // Give the cubicle back to the world
        // GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
        // Remove the cubicle from the list
        inventory.RemoveItem(target);
        return true;
    }
}