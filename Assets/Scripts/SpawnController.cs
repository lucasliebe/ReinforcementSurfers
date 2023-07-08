using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject truckPrefab;
    public GameObject rampPrefab;
    public GameObject coinPrefab;
    public GameObject jumpObstaclePrefab;
    public GameObject slideObstaclePrefab;
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

    public void triggerTruck(float? speed = null, float speedIncrement = 0 )
    {
        float truckSpeed = (speed == null) ? ground.speed : (float)speed;

        GameObject obstacle = Instantiate(truckPrefab, transform.position, Quaternion.identity, _trainingArea.transform);
        obstacle.GetComponent<ObstacleController>().SetSpeed(truckSpeed);
        obstacle.GetComponent<ObstacleController>().SetSpeedIncrement(speedIncrement);
    }
    
    public void triggerRamp()
    {
        GameObject obstacle = Instantiate(rampPrefab, transform.position, Quaternion.identity, _trainingArea.transform);
        obstacle.GetComponent<ObstacleController>().SetSpeed(ground.speed);
    }
    
    public void triggerCoin()
    {
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity, _trainingArea.transform);
        coin.GetComponent<ObstacleController>().SetSpeed(ground.speed);
    }

    public void triggerJumpObstacle()
    {
        GameObject jumpObstacle = Instantiate(jumpObstaclePrefab, transform.position - new Vector3(0f, 0.9f, 0f), Quaternion.identity, _trainingArea.transform);
        jumpObstacle.GetComponent<ObstacleController>().SetSpeed(ground.speed);
    }

    public void triggerSlideObstacle()
    {
        GameObject slideObstacle = Instantiate(slideObstaclePrefab, transform.position + new Vector3(0f, 0.95f, 0f), Quaternion.identity, _trainingArea.transform);
        slideObstacle.GetComponent<ObstacleController>().SetSpeed(ground.speed);
    }

    public void triggerFasterTruck()
    {
        triggerTruck(speed: ground.speed * 2);
    }
}
