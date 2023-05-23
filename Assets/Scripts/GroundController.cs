using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GroundController : MonoBehaviour
{
    public int lanes = 3;
    public float speed = 0.7f;
    public GameObject spawnerPrefab;
    private GameObject[] spawners;

    // Start is called before the first frame update
    void Start()
    {
        spawners = new GameObject[lanes];
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
        foreach (GameObject spawner in spawners)
        {
            if (rnd.Next(100) < 1)
            {
                spawner.GetComponent<SpawnController>().trigger();
            }
        }
    }
    
    public float getLaneDistance()
    {
        return transform.localScale.x / lanes;
    }
}
