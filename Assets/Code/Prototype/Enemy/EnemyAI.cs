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
    public Transform wallCheckPos;
    public LayerMask groundLayer;
    public GameObject EnemyBullet;
    PlayerController target;
    public float minFireDist;
    private bool mustTurn;
    public float fireRate;
    float nextFire;
    float tempSpeed;
    public bool Firing=false;
    Animator animator;


    
    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mustPatrol = true;
        nextFire = Time.time+fireRate;
        
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
        animator.SetFloat("Speed", rb.velocity.magnitude);
        animator.SetBool("Firing", Firing);

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

        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime,rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    private void stop()
    {
        tempSpeed = walkSpeed;
        walkSpeed = 0;
    }

    void go()
    {
        walkSpeed = tempSpeed;
    }


    IEnumerator waiter()
    {
        stop();
        yield return new WaitForSeconds(0.5f);
        go();
    }
    void CheckIfTimetToFire()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        

        if (Time.time > nextFire && distance < minFireDist)
        {
            Firing = true;
            
            StartCoroutine(waiter());
            StopCoroutine(waiter());
            
            Vector3 directionPlayerEnemy = target.transform.position - transform.position;
            float radianBullet = Mathf.Atan2(directionPlayerEnemy.y, directionPlayerEnemy.x);
            float angleBullet = radianBullet * Mathf.Rad2Deg;

            GameObject newProjectile = Instantiate(EnemyBullet);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleBullet);
            nextFire = Time.time + fireRate;
            Firing = false;
            animator.SetBool("Firing", Firing);
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
        if (other.gameObject.tag == "toxic cloud") {
            walkSpeed = 0;
            fireRate = 2000f;
        }
    }


}

