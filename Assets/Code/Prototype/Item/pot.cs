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
    public int ability;
    public int flesh;
    // public int fire;
    public int spirit;
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
        flesh = 0;
        ability = 0;
        // fire = 0;
        spirit = 0;
        thunder = 0;
        wind = 0;
    }

    //"fish", "horn", "feather", "skeleton", "carrot", "pearl", "mineral", "egg"
    //health, mana, exp, sword, skeleton, thunder, shield, swift

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Trophy"))
        {
            if (other.gameObject.GetComponent<trophy1>())
            {
                flesh += 3;
                ability += 1;
            }
            else if (other.gameObject.GetComponent<trophy2>())
            {
                thunder += 3;
            }
            else if (other.gameObject.GetComponent<trophy3>())
            {
                wind += 3;
            }
            else if (other.gameObject.GetComponent<trophy4>())
            {
                spirit += 5;
            }
            else if (other.gameObject.GetComponent<trophy5>())
            {
                flesh += 1;
                ability += 1;
                wind += 1;
            }
            else if (other.gameObject.GetComponent<trophy6>())
            {
                ability += 3;
                spirit += 2;
            }
            else if (other.gameObject.GetComponent<trophy7>())
            {
                gold += 5;
                thunder += 3;
            }
            else if (other.gameObject.GetComponent<trophy8>())
            {
                spirit += 3;
                ability += 3;
                flesh += 3;
            }
            Destroy(other.gameObject);
            
        }
    }

    public void refine()
    {
        if (gold >= 10 && wind >= 3 && flesh >= 5)
        {
            //sword
        }
        else if (spirit >= 5 && ability >= 3)
        {
            //skeleton
        }
        else if(thunder >= 3 && ability >= 3)
        {
            //thunder
        }
        else if (wind >= 5)
        {
            //swift
        }
        else if (flesh >= 3 && ability >= 2)
        {
            //shield
        }
        else if (spirit >= 2)
        {
            //exp
            Instantiate(BackPack.instance.props[2], dropPoint.position, Quaternion.identity);
        }
        else if (ability >= 2)
        {
            //mana
            Instantiate(BackPack.instance.props[1], dropPoint.position, Quaternion.identity);
        }
        else if (flesh >= 2)
        {
            //health
            Instantiate(BackPack.instance.props[0], dropPoint.position, Quaternion.identity);
        }
        clearPot();
    }
}
