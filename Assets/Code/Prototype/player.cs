using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    
    public float jumpSpeed = 0.4f;

    //Outlets
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        //move up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0.02f, 0);
        }
        //move down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -0.02f, 0);
        }

        //move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-0.02f, 0, 0);
        }
        //move Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0.02f, 0, 0);
        }


    }
}
