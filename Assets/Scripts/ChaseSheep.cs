using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseSheep : GAction {

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
        if (this.GetComponent<Wolf>().chaseSubject == null) {
            Debug.LogError("Chase subject not found");
            return false;
        }
        GameObject sheep = this.GetComponent<Wolf>().chaseSubject;

        Vector3 directionToPen = this.GetComponent<Wolf>().pen.transform.position - this.transform.position;
        Vector3 directionToSheep = sheep.transform.position - this.transform.position;

        // if angle to different stop chasing
        if (Vector3.Angle(directionToPen, directionToSheep) > 40) {
            Debug.Log("Angle to different");
            beliefs.RemoveState("ambushedSheep");
            beliefs.ModifyState("foundSheep", -1);
            return false;
        }

        GameObject pen = this.GetComponent<Wolf>().pen;

        // Instantiate an empty GameObject
        GameObject obj = new GameObject();

        obj.transform.position = PointToDirect(pen.transform.position, sheep.transform.position, 5f);

        target = obj;

        if (target == null)
            return false;
        return true;
    }

    public override bool PostPerform() {
        Destroy(target);
        // beliefs.RemoveState("danger");
        // beliefs.ModifyState("safe", 1);

        // // check for nearby Grass or Pond
        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, 30f);
        // foreach (var hitCollider in hitColliders) {
        //     Debug.Log("Collider: " + hitCollider.gameObject.tag);
        //     if (hitCollider.gameObject.tag == "Wolf") {
        //         if (beliefs.HasState("danger") && inventory.HasItem(hitCollider.gameObject)) {
        //             continue;
        //         }
        //         inventory.AddItem(hitCollider.gameObject);
        //         beliefs.ModifyState("danger", 1);
        //         beliefs.RemoveState("foundGrass");
        //         beliefs.RemoveState("foundPond");
        //         beliefs.RemoveState("safe");
        //         Debug.Log("Found Wolf");
        //     }
        // }

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
