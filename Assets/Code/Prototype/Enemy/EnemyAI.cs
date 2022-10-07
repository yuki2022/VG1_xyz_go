using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update

    public bool mustPatrol;
    public Rigidbody2D rb;
    public float walkSpeed;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public GameObject EnemyBullet;

    private bool mustTurn;
    float fireRate;
    float nextFire;
    void Start()
    {
        mustPatrol = true;
        fireRate = 5f;
        nextFire = Time.time+5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        CheckIfTimetToFire();
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustTurn)
        {
            Flip();
        }

        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime,rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    void CheckIfTimetToFire()
    {
        if (Time.time > nextFire)
        {
            GameObject newProjectile = Instantiate(EnemyBullet);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = Quaternion.identity;
            nextFire = Time.time + fireRate;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {

            Destroy(gameObject);
        }

        if (other.gameObject.tag == "ice")
        {
            walkSpeed = walkSpeed/2;
            fireRate = 1000f;
        }
    }


}

