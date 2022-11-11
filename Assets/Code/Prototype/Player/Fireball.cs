using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //Outlets
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right * 10f;
        Destroy(gameObject, 0.5f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Target>())
        {
            SoundManager.instance.PlaySoundHit();
            PlayerController.instance.exp++;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            SoundManager.instance.PlaySoundMiss();
        }
        
        //PlayerController.instance.EarnPoints(10);
        Destroy(gameObject);
    }
}
