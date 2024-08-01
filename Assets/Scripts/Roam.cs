using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Roam : GAction {

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


    public override bool PrePerform() {
        // Instantiate an empty GameObject
        GameObject obj = new GameObject();

        // put it in a random position inside the navigation mesh
        obj.transform.position = RandomNavmeshLocation(15f);

        target = obj;

        if (target == null)
            return false;
        return true;
    }

    public override bool PostPerform() {
        Destroy(target);

        // check for nearby Grass or Pond
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 30f);
        foreach (var hitCollider in hitColliders) {
            Debug.Log("Collider: " + hitCollider.gameObject.tag);
            if (hitCollider.gameObject.tag == "Grass") {
                // check if item already in inventory
                if (beliefs.HasState("foundGrass") && inventory.HasItem(hitCollider.gameObject)) {
                    continue;
                }
                inventory.AddItem(hitCollider.gameObject);
                beliefs.ModifyState("foundGrass", 1);
                Debug.Log("Found Grass");
            }
            if (hitCollider.gameObject.tag == "Pond") {
                if (beliefs.HasState("foundPond") && inventory.HasItem(hitCollider.gameObject)) {
                    continue;
                }
                inventory.AddItem(hitCollider.gameObject);
                beliefs.ModifyState("foundPond", 1);
                Debug.Log("Found Pond");
            }
            if (hitCollider.gameObject.tag == "Wolf") {
                if (beliefs.HasState("danger") && inventory.HasItem(hitCollider.gameObject)) {
                    continue;
                }
                inventory.AddItem(hitCollider.gameObject);
                beliefs.ModifyState("danger", 1);
                beliefs.RemoveState("foundGrass");
                beliefs.RemoveState("foundPond");
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
