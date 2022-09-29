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
    public int jumpsMax;

    //State Tracking
    int jumpsLeft;


    // Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(jumpsLeft > 0)
            {
                jumpsLeft--;
                _rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            }
        }

        //Move Player Left, dash when hold left shift
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _rb.AddForce(Vector2.left * 25f * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                _rb.AddForce(Vector2.left * 15f * Time.deltaTime, ForceMode2D.Impulse);
            }
            
        }

        //Move Player Right, dash when hold left shift
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _rb.AddForce(Vector2.right * 25f * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                _rb.AddForce(Vector2.right * 15f * Time.deltaTime, ForceMode2D.Impulse);
            }
            
        }

        //Thrust Forward (unfinished)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Use right as "forward" because our art faces to the right
            _rb.AddRelativeForce(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.5f);
            Debug.DrawRay(transform.position, Vector3.down * 0.7f);

            for(int i = 0; i< hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = jumpsMax;
                }
            }
        }
    }
}
