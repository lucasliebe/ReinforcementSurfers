using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private GameObject _trainingArea;
    private GroundController ground;
    
    // Start is called before the first frame update
    void Start()
    {
        _trainingArea = transform.parent.gameObject;
        ground = _trainingArea.transform.Find("Ground").GetComponent<GroundController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void trigger()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity, _trainingArea.transform);
        obstacle.GetComponent<ObstacleController>().speed = ground.speed;
    }
}
