public class EatGrass : GAction {

    public string actionName = "EatGrass";

    public override bool PrePerform() {
        target = inventory.FindItemWithTag("Grass");
        if (target == null)
            return false;
        return true;
    }

    public override bool PostPerform() {
        // Add a new state "TreatingPatient"
        beliefs.ModifyState("eatenGrass", 10);

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
