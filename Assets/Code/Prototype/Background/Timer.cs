using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public static Timer instance;

    public float timeElapsed;

    void awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }
}
