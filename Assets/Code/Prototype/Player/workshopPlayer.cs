using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class workshopPlayer : MonoBehaviour
{

    //Outlets
    Rigidbody2D _rb;
    SpriteRenderer sprite;

    //Configuration
    public int jumpsMax;
    public int healthMax;
    public float manaMax;

    public bool isPaused;


    //State Tracking
    int jumpsLeft;
    public Vector3 currentCheckpoint;


    // Methods
    void Start()
    {

        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentCheckpoint = transform.position;
    }

    void Update()
    {
        //pause
        if (isPaused)
        {
            return;
        }

        //k-->menu
        if (Input.GetKeyDown(KeyCode.K))
        {
            MenuController.instance.Show();
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsLeft > 0)
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

    }

    void OnCollisionEnter2D(Collision2D other)
    {


        if (other.gameObject.layer == LayerMask.NameToLayer("Trophy"))
        {
            if (other.gameObject.GetComponent<trophy1>())
            {
                BackPack.instance.AddTrophy(1);
            }
            else if (other.gameObject.GetComponent<trophy2>())
            {
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
            Destroy(other.gameObject);
        }

    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.5f);
            Debug.DrawRay(transform.position, Vector3.down * 0.7f);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = jumpsMax;
                }
            }
        }

    }


}
