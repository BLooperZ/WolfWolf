using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld {

    // Our GWorld instance
    private static readonly GWorld instance = new GWorld();
    // Our world states
    private static WorldStates world;
    // Queue of grass
    private static Queue<GameObject> grass;
    // // Queue of cubicles
    // private static Queue<GameObject> cubicles;

    static GWorld() {

        // Create our world
        world = new WorldStates();

        grass = new Queue<GameObject>();

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Grass");
        foreach (GameObject go in gos) {

            grass.Enqueue(go);
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
        grass.Enqueue(go);
    }

    public GameObject RemoveGrass() {
        if (grass.Count == 0) return null;
        return grass.Dequeue();
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
