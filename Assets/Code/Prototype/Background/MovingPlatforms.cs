using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public float speed;
    public Transform startPos;
    public Transform pos1,pos2;

    Vector3 nextPos;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;     
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == pos1.position)
            nextPos = pos2.position;
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
