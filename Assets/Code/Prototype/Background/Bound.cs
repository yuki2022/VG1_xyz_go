using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bound : MonoBehaviour
{
    public PlayerController player;
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<PlayerController>()){
            player.transform.position = player.currentCheckpoint;
            player.health = player.healthMax;
            player.healthbar.Sethealth(player.health);
        }
    }    
}
