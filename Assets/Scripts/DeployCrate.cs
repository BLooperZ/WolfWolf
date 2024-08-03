using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCrate : MonoBehaviour
{

    public GameObject cratePrefab;

    // Update is called once per frame
    void Update()
    {
        // when click left mouse button a crate is created on ground using navmesh
        if (Input.GetMouseButtonDown(0) && GWorld.Instance.GetCrateCount() < 1)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                UnityEngine.AI.NavMeshHit navHit;
                if (UnityEngine.AI.NavMesh.SamplePosition(hit.point, out navHit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
                {
                    GameObject crate = Instantiate(cratePrefab, navHit.position, Quaternion.identity);
                    crate.tag = "Crate";
                    GWorld.Instance.AddCrate(crate);
                }
            }
        }
    }
}
