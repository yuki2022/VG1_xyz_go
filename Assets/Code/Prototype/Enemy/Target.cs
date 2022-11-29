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
        StartCoroutine("ToxicTimer");
    }

    IEnumerator ToxicTimer()
    {
        yield return new WaitForSeconds(1f);

        if (isToxic)
        {
            if (health > 0)
            {
                health--;
            }
            else
            {
                die();
            }
        }

        StartCoroutine("ToxicTimer");
    }

    void OnCollisionEnter2D(Collision2D other) {
       
        if (other.gameObject.GetComponent<Fireball>()) {
            int damage = PlayerController.instance.abilityPower + 1;
            if (health > damage)
            {
                health -= damage;
            }
            else
            {
                die();
            }
        }

        if (other.gameObject.GetComponent<IceCone>())
        {
            int damage = PlayerController.instance.abilityPower + 1;
            if (health > damage)
            {
                health -= damage;
            }
            else
            {
                die();
            }
        }

        if (other.gameObject.GetComponent<Nocturne>())
        {
            if (health > 3)
            {
                health -= 3;
            }
            else
            {
                die();
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ToxicCloud>())
        {
            isToxic = true;
        }
    }

    void die()
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
