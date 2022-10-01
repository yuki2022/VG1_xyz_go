using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Configuration
    public int healthMax;
    public GameObject trophy;

    //State Tracking
    public int health;

    void Start()
    {
        health = healthMax;
    }

    void OnCollisionEnter2D(Collision2D other) {
       
        if (other.gameObject.GetComponent<Fireball>()) {
            if (health > 0)
            {
                health--;
            }
            else
            {
                Destroy(gameObject);
                GameObject newTrophy = Instantiate(trophy);
                newTrophy.transform.position = transform.position;
            }
        }
    }
    
}
