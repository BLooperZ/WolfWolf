using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Search : GAction {

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
        GameObject obj = new GameObject("SearchTarget");

        // put it in a random position inside the navigation mesh
        obj.transform.position = RandomNavmeshLocation(20f);

        target = obj;
        ephermal = true;

        if (target == null)
            return false;
        Invoke("Timeout", 30f);
        return true;
    }

    public override bool PostPerform() {
        Destroy(target);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100f);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.tag == "Sheep") {
                if (beliefs.HasState("foundSheep") && inventory.HasItem(hitCollider.gameObject)) {
                    continue;
                }
                inventory.AddItem(hitCollider.gameObject);
                beliefs.ModifyState("foundSheep", 1);
                Debug.Log("Found Sheep");
            }
            if (hitCollider.gameObject.tag == "Crate") {
                if (beliefs.HasState("foundCrate") && inventory.HasItem(hitCollider.gameObject)) {
                    continue;
                }
                inventory.AddItem(hitCollider.gameObject);
                beliefs.ModifyState("foundCrate", 1);
                Debug.Log("Found Crate");
            }
        }
        return true;
    }
}
