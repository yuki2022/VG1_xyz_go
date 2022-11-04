using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    //Outlets
    Rigidbody2D _rb;
    public Transform aimPivot;
    public GameObject fireball;
    public GameObject icecone;
    public GameObject ToxicCloud;
    SpriteRenderer sprite;
    public TMP_Text textScore;
    public TMP_Text textMoney;
    public HP_Bar healthbar;

    //Configuration
    public int jumpsMax;
    public int healthMax;
    public int manaMax;
    public int score;
    public int money;
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
        score = PlayerPrefs.GetInt("Score");
        money = PlayerPrefs.GetInt("Money");
    }

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        //update UI
        textScore.text = score.ToString();
        textMoney.text = money.ToString();

        //pause
        if (isPaused)
        {
            return;
        }

        //k-->menu
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

        //Prop 1: health bottle
        if (Input.GetKeyDown(KeyCode.Alpha1) && BackPack.instance.hasprop(1) && health < healthMax)
        {
            health ++;
            BackPack.instance.RemoveProp(1);
            healthbar.Sethealth(health);
        }

        //Prop 2: mana bottle
        if (Input.GetKeyDown(KeyCode.Alpha2) && BackPack.instance.hasprop(2) && mana < manaMax)
        {
            mana++;
            BackPack.instance.RemoveProp(2);
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
            if (other.gameObject.GetComponent<trophy1>())
            {
                BackPack.instance.AddTrophy(1);
            }
            else if (other.gameObject.GetComponent<trophy2>()) {
                BackPack.instance.AddTrophy(2);
            }
            else if (other.gameObject.GetComponent<trophy3>())
            {
                BackPack.instance.AddTrophy(3);
            }
            else if (other.gameObject.GetComponent<trophy4>())
            {
                BackPack.instance.AddTrophy(4);
            }
            else if (other.gameObject.GetComponent<trophy5>())
            {
                BackPack.instance.AddTrophy(5);
            }
            else if (other.gameObject.GetComponent<trophy6>())
            {
                BackPack.instance.AddTrophy(6);
            }
            else if (other.gameObject.GetComponent<trophy7>())
            {
                BackPack.instance.AddTrophy(7);
            }
            else if (other.gameObject.GetComponent<trophy8>())
            {
                BackPack.instance.AddTrophy(8);
            }
            money+=5;
            PlayerPrefs.SetInt("Money", PlayerController.instance.money);
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Prop"))
        {
            if (other.gameObject.GetComponent<manaBottle>())
            {
                BackPack.instance.AddProp(1);
            }
            else if (other.gameObject.GetComponent<healthBottle>())
            {
                BackPack.instance.AddProp(2);
            }
            else if (other.gameObject.GetComponent<EXPBottle>())
            {
                BackPack.instance.AddProp(3);
            }
            money+=2;
            PlayerPrefs.SetInt("Money", PlayerController.instance.money);
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
        money += pointAmount;
    }

    void UpdateDisplay()
    {
        textScore.text = score.ToString();
        textMoney.text = money.ToString();
    }


    public void ResetScore()
    {
        score = 0;
        PlayerPrefs.DeleteKey("Score");
    }

    public void ResetMoney()
    {
        money = 0;
        PlayerPrefs.DeleteKey("Money");
    }
}
