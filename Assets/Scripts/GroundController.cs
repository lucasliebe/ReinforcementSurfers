using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System.Linq;

public class GroundController : MonoBehaviour
{
    public int lanes = 3;
    public float speed = 1f;
    public GameObject spawnerPrefab;
    public GameObject trashcanPrefab;
    private SpawnController[] spawners;
    private GameObject trashcan;
    private bool[] lanesOccupied;
    private int resetTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    public void Setup()
    {
        spawners = new SpawnController[lanes];
        lanesOccupied = new bool[lanes];
        for (int i=0; i<lanes; i++)
        {
            int factor = i - (lanes / 2);
            float offset = transform.localScale.z / 2;
            GameObject spawner = Instantiate(spawnerPrefab, transform.position + new Vector3(0+getLaneDistance()*factor, 1.5f, offset), Quaternion.identity, transform.parent);
            spawner.transform.localScale += new Vector3(getLaneDistance(), 0, 0);
            spawners[i] = spawner.GetComponent<SpawnController>();
        }
        trashcan = Instantiate(trashcanPrefab, transform.position + new Vector3(0, 2.5f, -50), Quaternion.identity, transform.parent);
        trashcan.transform.localScale += new Vector3(transform.localScale.x, 0, 0);
    }

    public void Cleanup()
    {
        Time.timeScale = 0;
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
        }
        parentT.Find("Player").GetComponent<PlayerController>().Reset();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Random rnd = new Random();
        for (int i=0; i<lanes; i++)
        {
            if (lanesOccupied[i] == false)
            {
                if (rnd.Next(100) < 1)
                    spawners[i].triggerCoin();
            }
            if(lanesOccupied.Where(c => c).Count() == lanes - 1)
            {
                break;
            }
            if (rnd.Next(100) < 1)
            {
                spawners[i].triggerObstacle();
                lanesOccupied[i] = true;
                resetTimer = 0;
            }
        }
        if (resetTimer > 60)
        {
            speed += 0.01f;
            lanesOccupied = new bool[lanes];
        }
        resetTimer += 1;
    }
    
    public float getLaneDistance()
    {
        return transform.localScale.x / lanes;
    }
}
