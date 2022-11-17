using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Image manabar;

    //Configuration
    public int jumpsMax;
    public int healthMax;
    public float manaMax;
    public int money;
    public float fireRate = 0.5f;
    float nextFire = 0f;

    public bool isPaused;


    //State Tracking
    int jumpsLeft;
    public int health;
    public float mana;
    public int level;
    public int exp;
    public int levelUpThreshold;
    public Vector3 currentCheckpoint;


    // Methods
    void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentCheckpoint = transform.position;

        if (PlayerPrefs.HasKey("EXP")){
            exp = PlayerPrefs.GetInt("EXP");
        }
        else
        {
            exp = 0;
        }
        if (PlayerPrefs.HasKey("LV"))
        {
            level = PlayerPrefs.GetInt("LV"); 
        }
        else
        {
            exp = 0;
        }
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            exp = 0;
        }      

        healthMax = healthMax + level;
        manaMax += level;

        health = healthMax;
        mana = manaMax;
        manabar.fillAmount = mana / manaMax;
    }

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        //update UI
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
            manabar.fillAmount = mana / manaMax;
            GameObject newProjectile = Instantiate(icecone);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        //Ability 2: toxic cloud
        if (Input.GetKeyDown(KeyCode.W) & mana > 2)
        {
            mana -= 3;
            manabar.fillAmount = mana / manaMax;
            GameObject newProjectile = Instantiate(ToxicCloud);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
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
            manabar.fillAmount = mana / manaMax;
            BackPack.instance.RemoveProp(2);
        }

        //Prop 3: exp bottle
        if (Input.GetKeyDown(KeyCode.Alpha3) && BackPack.instance.hasprop(3))
        {
            exp += 10;
            updateExp();
            BackPack.instance.RemoveProp(3);
        }
    }


    void respawn()
    {
        transform.position = currentCheckpoint;
        health = healthMax;
        healthbar.Sethealth(health);
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
                respawn();

            
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
                respawn();
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
            if (other.gameObject.GetComponent<healthBottle>())
            {
                BackPack.instance.AddProp(1);
            }
            else if (other.gameObject.GetComponent<manaBottle>())
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


        if (collision.gameObject.GetComponent<Traps>())
        {
            if (health >= 2)
            {
                health = health - 1;
                SoundManager.instance.PlaySoundHurt();
                healthbar.Sethealth(health);
            }
            else
            {
                respawn();
            }
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
        exp += pointAmount;
        money += pointAmount;
    }

    void UpdateDisplay()
    {
        updateExp();
        textMoney.text = money.ToString();
    }

    void updateExp()
    {
        if (exp >= levelUpThreshold)
        {
            exp -= levelUpThreshold;
            levelUp();
        }

        textScore.text = "LV: " + level + " EXP: " + exp + "/" + levelUpThreshold;
    }

    void levelUp()
    {
        level += 1;
        levelUpThreshold += level * level * 5;
        healthMax += 1;
        manaMax += 1;
        health = healthMax;
        mana = manaMax;
        healthbar.Sethealth(health);
        manabar.fillAmount = mana / manaMax;
    }


    public void ResetLevel()
    {
        level = 0;
        PlayerPrefs.DeleteKey("LV");
    }

    public void ResetMoney()
    {
        money = 0;
        PlayerPrefs.DeleteKey("Money");
    }


}
