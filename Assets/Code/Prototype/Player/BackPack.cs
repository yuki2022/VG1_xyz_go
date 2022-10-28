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
    public Queue<int> myprops = new Queue<int>();

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTrophy(int x)
    {
        mytrophies.Enqueue(x);
        //print(x);
        //print(mytrophies);
    }

    public int RemoveTrophy()
    {
        var trophyidx = BackPack.instance.mytrophies.Dequeue();
        return trophyidx;
    }
}
