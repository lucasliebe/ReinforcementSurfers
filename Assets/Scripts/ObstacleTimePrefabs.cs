using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTimePrefabs : MonoBehaviour
{
    enum ObstacleType
    {
        None,
        Train,
        JumpObstacle,
        SlideObstacle,
        Coin
    }

    // List containing tuples of dictionaries of dictionaries
    // 1st tuple val.: length of prefab
    // 1st key: time of spawn
    // 2nd key: lane number
    // 2nd value: type of obstacle
    public List<(int, Dictionary<int, Dictionary<int, int>>)> obstacles = new List<(int, Dictionary<int, Dictionary<int, int>>)>()
    {
        (350, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {20, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.JumpObstacle}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Train}
                    }
                },
            }  
        ),

        // (400, new Dictionary<int, Dictionary<int, int>>()
        //     {
        //         {0, new Dictionary<int, int>()
        //             {
        //                 {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
        //             }
        //         },
        //         {20, new Dictionary<int, int>()
        //             {
        //                 {0, (int)ObstacleType.JumpObstacle}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
        //             }
        //         },
        //         {200, new Dictionary<int, int>()
        //             {
        //                 {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
        //             }
        //         },
        //         {250, new Dictionary<int, int>()
        //             {
        //                 {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Train}
        //             }
        //         },
        //     }  
        // ),

        (400, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Train}
                    }
                },
                {210, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Train}
                    }
                },
                {280, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.SlideObstacle}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                
            }  
        ),

        (400, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Train}
                    }
                },
                {140, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Coin}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {180, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Coin}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {210, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Train}
                    }
                },
                {220, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Coin}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {280, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.SlideObstacle}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                
            }  
        ),

        (400, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {20, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.SlideObstacle}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.JumpObstacle}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.SlideObstacle}, {2, (int)ObstacleType.None}
                    }
                },
                {260, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Train}
                    }
                },
                
            }  
        ),

        (300, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Coin}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {40, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Coin}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {80, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Coin}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {120, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Coin}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {160, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Coin}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Coin}
                    }
                },
                {240, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Coin}
                    }
                },
                {280, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Coin}
                    }
                },
            }  
        ),
        
    };

   
}
