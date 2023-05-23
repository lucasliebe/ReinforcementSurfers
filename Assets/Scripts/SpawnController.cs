using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private GroundController ground;
    
    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.Find("Ground").GetComponent<GroundController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void trigger()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
        obstacle.GetComponent<ObstacleController>().speed = ground.speed;
    }
}
