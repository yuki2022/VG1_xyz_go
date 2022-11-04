using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    public static BackPack instance;

    //State Tracking
    public GameObject[] trophies;
    public GameObject[] props;
    public Queue<int> mytrophies = new Queue<int>();
    int[] myprops;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        myprops = new int[props.Length];
        for (int i = 0; i < myprops.Length; i++)
        {
            myprops[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTrophy(int x)
    {
        x--;
        mytrophies.Enqueue(x);
    }

    public int RemoveTrophy()
    {
        var trophyidx = BackPack.instance.mytrophies.Dequeue();
        return trophyidx;
    }

    public void AddProp(int x)
    {
        x--;
        myprops[x] ++;
    }

    public void RemoveProp(int x)
    {
        x--;
        myprops[x] --;
    }

    public bool hasprop(int x)
    {
        x--;
        return myprops[x] > 0;
    }
}
