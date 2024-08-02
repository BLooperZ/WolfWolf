using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public int Boundary = 50;
    public int speed = 5;

    private int theScreenWidth;
    private int theScreenHeight;

    void Start() 
    {
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
    }

    void Update() 
    {
        if (Input.mousePosition.x > theScreenWidth - Boundary || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        
        if (Input.mousePosition.x < 0 + Boundary || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        
        if (Input.mousePosition.y > theScreenHeight - Boundary || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
        
        if (Input.mousePosition.y < 0 + Boundary|| Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
        
    }	
}
