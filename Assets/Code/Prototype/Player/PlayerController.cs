using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
//    MenuController menuControllerComponent;
    public static PlayerController instance;

    //Outlets
    Rigidbody2D _rb;
    public Transform aimPivot;
    public GameObject fireball;
    public GameObject icecone;
    public GameObject ToxicCloud;
    SpriteRenderer sprite;
    public TMP_Text textScore;
    public HP_Bar healthbar;

    //Configuration
    public int jumpsMax;
    public int healthMax;
    public int manaMax;
    public int score;
    public float fireRate = 0.5f;
    float nextFire = 0f;
    public bool isPaused;


    //State Tracking
    int jumpsLeft;
    public int health;
    int mana;
    public Vector3 currentCheckpoint;


    // Methods
    void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentCheckpoint = transform.position;
        health = healthMax;
        mana = manaMax;
        score = 0;
    }

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        //pause
        if (isPaused)
        {
            return;
        }

        //Escape-->menu
        if (Input.GetKeyDown(KeyCode.K)) {
            MenuController.instance.Show();
        }
        UpdateDisplay();

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
            sprite.flipX = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _rb.AddForce(Vector2.left * 30f * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                _rb.AddForce(Vector2.left * 15f * Time.deltaTime, ForceMode2D.Impulse);
            }
            
        }

        //Move Player Right, dash when hold left shift
        if (Input.GetKey(KeyCode.D))
        {
            sprite.flipX = false;
            if (Input.GetKey(KeyCode.RightShift))
            {
                _rb.AddForce(Vector2.right * 30f * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                _rb.AddForce(Vector2.right * 15f * Time.deltaTime, ForceMode2D.Impulse);
            }
            
        }

        //Aim Toward Mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        //Normal attack: fireball
        if (Input.GetMouseButtonDown(0) && Time.time >nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject newProjectile = Instantiate(fireball);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        //Ability 1: ice cone
        if (Input.GetKeyDown(KeyCode.Q) & mana > 0)
        {
            mana--;
            GameObject newProjectile = Instantiate(icecone);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        //Ability 2: toxic cloud
        if (Input.GetKeyDown(KeyCode.W) & mana > 2)
        {
            mana -= 3;
            GameObject newProjectile = Instantiate(ToxicCloud);
            newProjectile.transform.position = transform.position;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<EnemyBullet>())
        {
            if (health > 1)
            {
                health--;
                
                healthbar.Sethealth(health);
            }
            else
            {
                transform.position = currentCheckpoint;
                health = healthMax;
                healthbar.Sethealth(health);
                PlayerController.instance.score-=5;
            }
        }
        if (other.gameObject.GetComponent<EnemyAI>())
        {
            if (health > 4)
            {
                health=health-3;
                SoundManager.instance.PlaySoundHurt();
                healthbar.Sethealth(health);
            }
            else
            {
                transform.position = currentCheckpoint;
                health = healthMax;
                healthbar.Sethealth(health);
                PlayerController.instance.score -= 5;
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Trophy"))
        {
            if (other.gameObject.GetComponent<manaBottle>())
            {
                if (mana < manaMax)
                {
                    mana++;
                }
                BackPack.instance.AddTrophy(1);
            }
            else if (other.gameObject.GetComponent<healthBottle>()) {
                if (health < healthMax)
                {
                    health++;
                }
                BackPack.instance.mytrophies.Enqueue(2);
            }
            else if (other.gameObject.GetComponent<EXPBottle>())
            {
                if (health < healthMax)
                {
                    health++;
                }
                BackPack.instance.mytrophies.Enqueue(3);
            }

            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Prop"))
        {
            //GameObject otherClone = Instantiate(other.gameObject);
            //BackPack.instance.props.Add(other.gameObject);
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform.position;
            collision.GetComponent<Collider2D>().enabled = false;
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

    public void EarnPoints(int pointAmount) {
        score += pointAmount;
    }

    void UpdateDisplay()
    {
        textScore.text = score.ToString();//textMoney.text = money.ToString();
    }
}
