using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkShopController : MonoBehaviour
{

    public Transform dropPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (BackPack.instance.trophies.Count > 0) {
            Instantiate(BackPack.instance.trophies[0], dropPoint);
        }
    }
}
