using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Target : MonoBehaviour
{
    //Configuration
    public int healthMax;
    public GameObject[] trophies;

    //State Tracking
    public int health;
    public bool isToxic;

    void Start()
    {
        health = healthMax;
        isToxic = false;
    }

    private void FixedUpdate()
    {
        if(Time.fixedDeltaTime == 1 && isToxic)
        {
            if (health > 0)
            {
                health --;
            }
            else
            {
                int trophyidx = Random.Range(0, trophies.Length);
                GameObject trophy = trophies[trophyidx];
                PlayerController.instance.exp += 3;
                PlayerPrefs.SetInt("EXP", PlayerController.instance.exp);
                Destroy(gameObject);
                GameObject newTrophy = Instantiate(trophy);
                newTrophy.transform.position = transform.position;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
       
        if (other.gameObject.GetComponent<Fireball>()) {
            if (health > 1)
            {
                health--;
            }
            else
            {
                int trophyidx = Random.Range(0, trophies.Length);
                GameObject trophy = trophies[trophyidx];
                PlayerController.instance.exp += 3;
                PlayerPrefs.SetInt("EXP", PlayerController.instance.exp);
                //create an explosion
                GameObject explosion = Instantiate(PlayerController.instance.fireballPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                GameObject newTrophy = Instantiate(trophy);
                newTrophy.transform.position = transform.position;
            }
        }

        if (other.gameObject.GetComponent<IceCone>())
        {
            if (health > 1)
            {
                health--;
            }
            else
            {
                int trophyidx = Random.Range(0, trophies.Length);
                GameObject trophy = trophies[trophyidx];
                PlayerController.instance.exp += 3;
                PlayerPrefs.SetInt("EXP", PlayerController.instance.exp);
                Destroy(gameObject);
                GameObject newTrophy = Instantiate(trophy);
                newTrophy.transform.position = transform.position;
            }
        }
        if (other.gameObject.GetComponent<ToxicCloud>())
        {
            isToxic = true;
            /* if (health > 2)
            {
                health-=3;
                
            }
            else
            {
                int trophyidx = Random.Range(0, trophies.Length);
                GameObject trophy = trophies[trophyidx];
                PlayerController.instance.exp += 3;
                PlayerPrefs.SetInt("EXP", PlayerController.instance.exp);
                Destroy(gameObject);
                GameObject newTrophy = Instantiate(trophy);
                newTrophy.transform.position = transform.position;
            } */
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ToxicCloud>())
        {
            if (health > 1)
            {
                health--;
            }
            else
            {
                int trophyidx = Random.Range(0, trophies.Length);
                GameObject trophy = trophies[trophyidx];
                PlayerController.instance.exp += 3;
                PlayerPrefs.SetInt("EXP", PlayerController.instance.exp);
                Destroy(gameObject);
                GameObject newTrophy = Instantiate(trophy);
                newTrophy.transform.position = transform.position;
            }
        }
    }

}
