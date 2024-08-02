using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld {

    // Our GWorld instance
    private static readonly GWorld instance = new GWorld();
    // Our world states
    private static WorldStates world;
    // Queue of grass
    private static HashSet<GameObject> grass;

    private static HashSet<GameObject> crates;
    // // Queue of cubicles
    // private static Queue<GameObject> cubicles;

    static GWorld() {

        // Create our world
        world = new WorldStates();

        grass = new HashSet<GameObject>();
        crates = new HashSet<GameObject>();

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Grass");
        foreach (GameObject go in gos) {

            grass.Add(go);
        }
        // // Create patients array
        // patients = new Queue<GameObject>();
        // // Create cubicles array
        // cubicles = new Queue<GameObject>();
        // // Find all GameObjects that are tagged "Cubicle"
        // GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
        // // Then add them to the cubicles Queue
        // foreach (GameObject c in cubes) {

        //     cubicles.Enqueue(c);
        // }

        // // Inform the state
        // if (cubes.Length > 0) {
        //     world.ModifyState("FreeCubicle", cubes.Length);
        // }

        // // Set the time scale in Unity
        // Time.timeScale = 5.0f;
    }

    private GWorld() {

    }

    public void AddGrass(GameObject go) {
        grass.Add(go);
    }

    public void RemoveGrass(GameObject go) {
        grass.Remove(go);
    }

    public GameObject GetGrassWithin(float radius, Vector3 pos) {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in grass) {
            float d = Vector3.Distance(go.transform.position, pos);
            if (d < distance) {
                closest = go;
                distance = d;
            }
        }
        RemoveGrass(closest);
        return closest;
    }

    public int GetCrateCount() {
        return crates.Count;
    }

    public void AddCrate(GameObject go) {
        crates.Add(go);
    }

    public void RemoveCrate(GameObject go) {
        crates.Remove(go);
    }

    // // Add patient
    // public void AddPatient(GameObject p) {

    //     // Add the patient to the patients Queue
    //     patients.Enqueue(p);
    // }

    // // Remove patient
    // public GameObject RemovePatient() {

    //     if (patients.Count == 0) return null;
    //     return patients.Dequeue();
    // }

    // // Add cubicle
    // public void AddCubicle(GameObject p) {

    //     // Add the patient to the patients Queue
    //     cubicles.Enqueue(p);
    // }

    // // Remove cubicle
    // public GameObject RemoveCubicle() {

    //     // Check we have something to remove
    //     if (cubicles.Count == 0) return null;
    //     return cubicles.Dequeue();
    // }

    public static GWorld Instance {

        get { return instance; }
    }

    public WorldStates GetWorld() {

        return world;
    }
}
