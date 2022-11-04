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
        if (BackPack.instance.mytrophies != null && BackPack.instance.mytrophies.Count > 0) {
            var trophyidx = BackPack.instance.RemoveTrophy();
            Instantiate(BackPack.instance.trophies[trophyidx], dropPoint.position, Quaternion.identity);
        }
    }
}
