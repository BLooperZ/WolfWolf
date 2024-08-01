using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Flee : GAction {

    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }

    // Claculate a position on the NavMesh far away from the wolf within radius
    public Vector3 AwayFromWolf(Vector3 wolfPosition, Vector3 currentPostion, float radius) {
        Vector3 direction = currentPostion - wolfPosition;
        direction.Normalize();
        direction *= radius;
        Vector3 destination = direction + currentPostion;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (UnityEngine.AI.NavMesh.SamplePosition(destination, out hit, radius, 1)) {
            finalPosition = hit.position;            
        } else {
            finalPosition = RandomNavmeshLocation(radius);
        }
        return finalPosition;
    }

    public override bool PrePerform() {
        GameObject wolf = inventory.FindItemWithTag("Wolf");

        // Check if wolf is null
        if (wolf == null) {
            Debug.LogError("Wolf not found in inventory");
            return false;
        }

        inventory.RemoveItem(wolf);

        // Instantiate an empty GameObject
        GameObject obj = new GameObject();

        obj.transform.position = AwayFromWolf(wolf.transform.position, this.transform.position, 30f);

        target = obj;

        if (target == null)
            return false;
        return true;
    }

    public override bool PostPerform() {
        Destroy(target);
        beliefs.RemoveState("danger");
        beliefs.ModifyState("safe", 1);

        // check for nearby Grass or Pond
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 30f);
        foreach (var hitCollider in hitColliders) {
            Debug.Log("Collider: " + hitCollider.gameObject.tag);
            if (hitCollider.gameObject.tag == "Wolf") {
                if (beliefs.HasState("danger") && inventory.HasItem(hitCollider.gameObject)) {
                    continue;
                }
                inventory.AddItem(hitCollider.gameObject);
                beliefs.ModifyState("danger", 1);
                beliefs.RemoveState("foundGrass");
                beliefs.RemoveState("foundPond");
                beliefs.RemoveState("safe");
                Debug.Log("Found Wolf");
            }
        }

        // Add a new state "TreatingPatient"
        // beliefs.ModifyState("foundGrass", 1);
        // Hide grass until grown again
        // // Give back the cubicle
        // GWorld.Instance.AddCubicle(target);
        // // Give the cubicle back to the world
        // GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
        // Remove the cubicle from the list
        return true;
    }
}
