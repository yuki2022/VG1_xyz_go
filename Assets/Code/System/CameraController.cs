using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPosition;
    public float offsetSmothing;

    // Start is called before the first frame update
    void Start()
    {
            //playerPosition = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmothing * Time.deltaTime);
    }
}
