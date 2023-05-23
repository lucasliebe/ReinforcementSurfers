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
    private GameObject[] spawners;
    private bool[] lanesOccupied;
    private int resetTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawners = new GameObject[lanes];
        lanesOccupied = new bool[lanes];
        for (int i=0; i<lanes; i++)
        {
            int factor = i - (lanes / 2);
            float offset = transform.localScale.z - transform.position.z;
            GameObject spawner = Instantiate(spawnerPrefab, new Vector3(0+getLaneDistance()*factor, 1.5f, offset), Quaternion.identity);
            spawner.transform.localScale += new Vector3(getLaneDistance(), 0, 0);
            spawners[i] = spawner;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Random rnd = new Random();
        for (int i=0; i<lanes; i++)
        {
            if(lanesOccupied.Where(c => c).Count() == lanes - 1)
            {
                break;
            }
            if (rnd.Next(100) < 1)
            {
                spawners[i].GetComponent<SpawnController>().trigger();
                lanesOccupied[i] = true;
                resetTimer = 0;
            }
        }
        if (resetTimer > 60)
        {
            lanesOccupied = new bool[lanes];
        }
        resetTimer += 1;
    }
    
    public float getLaneDistance()
    {
        return transform.localScale.x / lanes;
    }
}
