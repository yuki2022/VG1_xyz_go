using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bound : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<PlayerController>()){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }    
}
