using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    //Outlet
    public GameObject[] props;
    public Transform dropPoint;

    //State Tracking
    public int gold;
    public int wood;
    public int water;
    public int fire;
    public int ground;
    public int thunder;
    public int wind;

    // Start is called before the first frame update
    void Start()
    {
        clearPot();
    }

    void clearPot()
    {
        gold = 0;
        wood = 0;
        water = 0;
        fire = 0;
        ground = 0;
        thunder = 0;
        wind = 0;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Trophy"))
        {
            Destroy(other.gameObject);
        }
    }

    public void refine()
    {
        Instantiate(BackPack.instance.props[0], dropPoint.position, Quaternion.identity);
        clearPot();
    }
}
