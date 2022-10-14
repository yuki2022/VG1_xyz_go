using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trophy : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            SoundManager.instance.PlaySoundHeal();
            Destroy(gameObject);           
        }
    }
}
