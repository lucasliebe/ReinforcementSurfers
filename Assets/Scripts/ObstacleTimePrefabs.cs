using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTimePrefabs : MonoBehaviour
{
    // List containing tuples of dictionaries of dictionaries
    // 1st tuple val.: max. time steps ( == last key)
    // 1st key: time of spawn
    // 2nd key: lane number
    // 2nd value: type of obstacle
    public List<(int, Dictionary<int, Dictionary<int, int>>)> obstacles = new List<(int, Dictionary<int, Dictionary<int, int>>)>()
    {
        (500, new Dictionary<int, Dictionary<int, int>>()
            {
                {100, new Dictionary<int, int>()
                    {
                        {0, 1}, {1, -1}, {2, -1}
                    }
                },
                {300, new Dictionary<int, int>()
                    {
                        {0, 1}, {1, -1}, {2, -1}
                    }
                },
                {500, new Dictionary<int, int>()
                    {
                        {0, 1}, {1, -1}, {2, -1}
                    }
                }
            }  
        ),
        
    };

   
}
