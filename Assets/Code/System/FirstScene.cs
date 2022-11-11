using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            // PlayerPrefs.SetInt("Health", PlayerController.instance.health);
            SceneManager.LoadScene("Prototype");
        }
    }
}
