using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nocturne : MonoBehaviour
{
    //Outlets
    Rigidbody2D _rigidbody2D;

    //Configuration
    public int healthMax;

    //State Tracking
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //_rigidbody2D.velocity = transform.right * 7.5f;
        health = healthMax;
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        transform.position += new Vector3(0.01f, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.GetComponent<EnemyBullet>())
        {
            Destroy(other.gameObject);
            if (health > 0)
            {
                health--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.GetComponent<Target>())
        {
            Destroy(gameObject);
        }
    }
}
