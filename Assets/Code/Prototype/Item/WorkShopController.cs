using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkShopController : MonoBehaviour
{
    //outlet
    public Transform dropPoint;
    public TMP_Text currentMat;

    //state tracking
    public string[] trophyName;

    void Start()
    {
        trophyName = new string[] { "fish", "horn", "feather", "skeleton", "carrot", "pearl", "mineral", "egg"};
        updateMat();
    }

    public void updateMat()
    {
        if (BackPack.instance.mytrophies != null && BackPack.instance.mytrophies.Count > 0)
        {
            var trophyidx = BackPack.instance.GetFirst();
            currentMat.text = "Onhold Material: " + trophyName[trophyidx];
        }
        else
        {
            currentMat.text = "Backpack is Empty";
        }

    }

    public void drop()
    {
        if (BackPack.instance.mytrophies != null && BackPack.instance.mytrophies.Count > 0) {
            var trophyidx = BackPack.instance.RemoveTrophy();
            Instantiate(BackPack.instance.trophies[trophyidx], dropPoint.position, Quaternion.identity);
            updateMat();
        }
    }

    public void skip()
    {
        if (BackPack.instance.mytrophies != null && BackPack.instance.mytrophies.Count > 0)
        {
            BackPack.instance.SkipTrophy();
            updateMat();
        }
    }
}
