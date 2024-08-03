﻿using System.Collections;
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
        GameObject obj = new GameObject("RoamTarget");

        // put it in a random position inside the navigation mesh
        obj.transform.position = RandomNavmeshLocation(5f);

        target = obj;
        ephermal = true;

        if (target == null)
            return false;

        Invoke("Timeout", 30f);
        return true;
    }

    public override bool PostPerform() {
        Destroy(target);

        // check for nearby Grass or Pond

        GameObject grass = GWorld.Instance.GetGrassWithin(10f, this.transform.position);
        if (grass != null) {
            inventory.AddItem(grass);
            beliefs.ModifyState("foundGrass", 1);
            Debug.Log("Found Grass");
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (var hitCollider in hitColliders) {
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

        return true;
    }
}
