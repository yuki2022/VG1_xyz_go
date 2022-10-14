using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = GameObject.FindObjectOfType<PlayerController>().healthMax;
        healthBar.value = GameObject.FindObjectOfType<PlayerController>().healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sethealth(int hp)
    {
        healthBar.value = hp;
    }
}
