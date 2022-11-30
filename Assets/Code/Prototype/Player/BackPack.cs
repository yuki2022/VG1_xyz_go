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
        // If no Player ever existed, we are it.
        if (instance == null)
            instance = this;
        // If one already exist, it's because it came from another level.
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

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

    public void SkipTrophy()
    {
        var trophyidx = RemoveTrophy();
        mytrophies.Enqueue(trophyidx);
    }

    public int GetFirst()
    {
        var trophyidx = BackPack.instance.mytrophies.Peek();
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
