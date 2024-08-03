﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AmbushSheep : GAction {

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

    public Vector3 PointToDirect(Vector3 penPosition, Vector3 sheepPosition, float radius) {
        Vector3 direction = sheepPosition - penPosition;
        direction.Normalize();
        direction *= radius;
        Vector3 destination = direction + sheepPosition;
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
        GameObject sheep = inventory.FindItemWithTag("Sheep");
        beliefs.ModifyState("foundSheep", -1);

        // Check if wolf is null
        if (sheep == null) {
            Debug.LogError("Sheep not found in inventory");
            return false;
        }

        inventory.RemoveItem(sheep);

        GameObject pen = this.GetComponent<Wolf>().pen;

        // Instantiate an empty GameObject
        GameObject obj = new GameObject();

        obj.transform.position = PointToDirect(pen.transform.position, sheep.transform.position, 10f);

        target = obj;

        if (target == null)
            return false;

        
        this.GetComponent<Wolf>().chaseSubject = sheep;
        return true;
    }

    public override bool PostPerform() {
        Destroy(target);
        beliefs.ModifyState("ambushedSheep", 1);
        return true;
    }
}
