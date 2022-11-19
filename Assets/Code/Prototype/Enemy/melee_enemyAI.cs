using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class melee_enemyAI : MonoBehaviour
{
    // Start is called before the first frame update

    public bool mustPatrol;
    public Rigidbody2D rb;
    public float walkSpeed;
    public Transform groundCheckPos;
    public Transform wallCheckPos;
    public GameObject hitbox;
    public LayerMask groundLayer;
    public GameObject EnemyBullet;
    PlayerController target;
    public float minFireDist;
    private bool mustTurn;
    public float fireRate;
    float nextFire;
    float tempSpeed;
    public bool Firing = false;
    Animator animator;



    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mustPatrol = true;
        nextFire = Time.time + fireRate;
        tempSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        CheckIfTimetToFire();

        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > minFireDist)
        {
            Firing = false;
            animator.SetBool("attack", Firing);
            go();
        }
    }

    private void FixedUpdate()
    {
        
        animator.SetBool("attack", Firing);

        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(wallCheckPos.position, 0.2f, groundLayer);
        }

    }

    void Patrol()
    {
        if (mustTurn)
        {
            Flip();
        }

        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        tempSpeed *= -1;
        mustPatrol = true;
    }

    private void stop()
    {
  
        walkSpeed = 0;
    }

    void go()
    {
        walkSpeed = tempSpeed;
    }


    IEnumerator waiter()
    {

        yield return new WaitForSeconds(0.5f);



        
    }
    void CheckIfTimetToFire()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);


        if (Time.time > nextFire && distance < minFireDist)
        {

            Firing = true;
            animator.SetBool("attack", Firing);
            stop();

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
            walkSpeed = walkSpeed / 2;
            fireRate = 1000f;
        }
        if (other.gameObject.tag == "toxic cloud")
        {
            walkSpeed = 0;
            fireRate = 2000f;
        }
    }



}


