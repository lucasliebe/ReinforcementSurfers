using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System.Linq;
using System;

public class GroundController : MonoBehaviour
{
    public int lanes = 3;
    public float speed = 1f;
    public float speedIncrement = 0.0f;
    private float startSpeed;
    public GameObject spawnerPrefab;
    public GameObject trashcanPrefab;
    private SpawnController[] spawners;
    private GameObject trashcan;
    private int[] lanesOccupied;
    private int resetTimer = 0;
    private int prefabSpawnTimer = 0;
    private int randomSpawnTimer = 0;
    private bool canSpawn = true;
    private Random rnd = new Random();
    ObstacleTimePrefabs obstacleTimePrefabs = new ObstacleTimePrefabs();
    private int decision = 0;
    (int, Dictionary<int, Dictionary<int, int>>) randomObstacle;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    public void Setup()
    {
        startSpeed = speed;
        spawners = new SpawnController[lanes];
        lanesOccupied = new int[lanes];
        Array.Fill(lanesOccupied, -1);
        for (int i=0; i<lanes; i++)
        {
            int factor = i - (lanes / 2);
            float offset = transform.localScale.z / 2;
            GameObject spawner = Instantiate(spawnerPrefab, transform.position + new Vector3(0+getLaneDistance()*factor, 1.5f, offset), Quaternion.identity, transform.parent);
            spawner.transform.localScale += new Vector3(getLaneDistance(), 0, 0);
            spawners[i] = spawner.GetComponent<SpawnController>();
        }
        trashcan = Instantiate(trashcanPrefab, transform.position + new Vector3(0, 2.5f, -40), Quaternion.identity, transform.parent);
        trashcan.transform.localScale += new Vector3(transform.localScale.x, 0, 0);
    }

    public void Cleanup()
    {
        // Time.timeScale = 0;
        foreach (var spawner in spawners)
        {
            Destroy(spawner.gameObject);
        }
        Destroy(trashcan);
        Transform parentT = transform.parent;
        foreach (Transform childT in parentT)
        {
            if(childT.CompareTag("Obstacle")) Destroy(childT.gameObject);
            if(childT.CompareTag("Coin")) Destroy(childT.gameObject);
            if(childT.CompareTag("JumpObstacle")) Destroy(childT.gameObject);
            if(childT.CompareTag("SlideObstacle")) Destroy(childT.gameObject);
        }
        parentT.Find("Player").GetComponent<PlayerController>().Reset();
        speed = startSpeed;
        // Time.timeScale = 1;
    }

    private int RoundToTen(int number)
    {
        return (int)(Math.Round(number / 10.0) * 10);
    }

    private void SpawnPrefabObstacles(Dictionary<int, Dictionary<int, int>> obstacles)
    {
        Debug.Log(RoundToTen(prefabSpawnTimer));
        try 
        {
            var obstaclesOnLane = obstacles[RoundToTen(prefabSpawnTimer)];
            foreach (var obstacle in obstaclesOnLane)
            {
                switch (obstacle.Value)
                {
                    case 0: 
                        break;
                    case 1:
                        spawners[obstacle.Key].triggerObstacle();
                        break;
                    case 2:
                        spawners[obstacle.Key].triggerJumpObstacle();
                        break;
                    case 3:
                        spawners[obstacle.Key].triggerSlideObstacle();
                        break;
                    case 4:
                        Debug.Log("Coin");
                        spawners[obstacle.Key].triggerCoin();
                        break;
                }
            }
        }
        catch (Exception) {}

        prefabSpawnTimer += 10;
    }

    private void SpawnObstacles()
    {
        int spawnedObstacles = 0;
        for (int i = 0; i < lanes; i++)
        {
            if (lanesOccupied[i] == 0)
            {
                spawnedObstacles++;
            }
        }
        
        for (int i=0; i<lanes; i++)
        {
            if (rnd.Next(100) > 25) continue;
            if (lanesOccupied[i] != -1) continue;

            int obstacleType = rnd.Next(4);
            switch (obstacleType)
            {
                case 0:
                    if (spawnedObstacles >= lanes - 1) continue;
                    spawners[i].triggerObstacle();
                    lanesOccupied[i] = 0;
                    spawnedObstacles++;
                    break;
                case 1:
                    spawners[i].triggerJumpObstacle();
                    lanesOccupied[i] = 1;
                    break;
                case 2:
                    spawners[i].triggerSlideObstacle();
                    lanesOccupied[i] = 2;
                    break;
                case 3:
                    spawners[i].triggerCoin();
                    lanesOccupied[i] = 3;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (decision == 0)
        {
            // Distance between intervals of obstacles
            resetTimer += 1;
            if (resetTimer > (int)(18 / speed))
            {
                decision = rnd.Next(1, 3);
                resetTimer = 0;
                lanesOccupied = new int[lanes];
                Array.Fill(lanesOccupied, -1);
                int randomIndex = rnd.Next(obstacleTimePrefabs.obstacles.Count);
                randomObstacle = obstacleTimePrefabs.obstacles[randomIndex];
            }
        }
        
        // Spawn random prefab obstacles
        if (decision == 1)
        {
            SpawnPrefabObstacles(randomObstacle.Item2);
            if (prefabSpawnTimer >= randomObstacle.Item1) 
            {
                prefabSpawnTimer = 0;
                decision = 0;
            }
        }
        // Spawn row of random obstacles
        else if (decision == 2)
        {
            randomSpawnTimer += 1;
            SpawnObstacles();
            if (randomSpawnTimer > (int)(2.5f / speed))
            {
                randomSpawnTimer = 0;
                decision = 0;
            }
        }
        
    }
    
    public float getLaneDistance()
    {
        return transform.localScale.x / lanes;
    }
}
