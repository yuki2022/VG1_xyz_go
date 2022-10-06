using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<<< HEAD:Assets/Code/Prototype/Player/IceCone.cs
public class IceCone : MonoBehaviour
========
public class Fireball : MonoBehaviour
>>>>>>>> 0736e919deec8f1f812814ef05a7afa163abd2e9:Assets/Code/Prototype/Player/Fireball.cs
{
    //Outlets
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right * 10f;
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
