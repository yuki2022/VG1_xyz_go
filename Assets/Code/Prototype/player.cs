using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    //Outlets
    Rigidbody2D _rb;

    //Configuration
    public float speed ;
    public float rotationSpeed ;

    // Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //move Up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0.02f, 0);
        }

        //move Down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -0.02f, 0);
        }
        //Turn Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.AddTorque(rotationSpeed * Time.deltaTime);
        }

        //Turn Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb.AddTorque(-rotationSpeed * Time.deltaTime);
        }

        //Thrust Forward
        if (Input.GetKey(KeyCode.Space))
        {
            //Use right as "forward" because our art faces to the right
            _rb.AddRelativeForce(Vector2.right * speed * Time.deltaTime);
        }
    }
}
