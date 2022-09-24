using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Outlets
    Rigidbody2D _rb;

    //Configuration
    public float speed;
    public float rotationSpeed;

    //State Tracking

    // Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //jump
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += new Vector3(0, 0.02f, 0);
        }

        //Move Player Left
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
        }

        //Move Player Right
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
        }

        //Thrust Forward (unfinished)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Use right as "forward" because our art faces to the right
            _rb.AddRelativeForce(Vector2.right * speed * Time.deltaTime);
        }
    }
}
