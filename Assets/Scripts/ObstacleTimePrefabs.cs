using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTimePrefabs
{
    enum ObstacleType
    {
        None,
        Train,
        JumpObstacle,
        SlideObstacle,
        Coin,
        Ramp,
        CoinUpper,
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
        // "Tunnel" Obstacle left
        (650, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {100, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {300, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {500, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {600, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
            }  
        ),
        // "Tunnel" Obstacle right
        (650, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {100, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {300, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {500, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {600, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Train}
                    }
                },
            }
        ),
        // "Tunnel" with extra obstacles on the way
        (650, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {100, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.SlideObstacle}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {300, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.JumpObstacle}
                    }
                },
                {400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {500, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {600, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
            }  
        ),
        // Only a ramp out, can jump over gap
        (700, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.SlideObstacle}, {2, (int)ObstacleType.Ramp}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Train}
                    }
                },
                {400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {500, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Train}
                    }
                },
            }  
        ),
        // Only a ramp out, can not jump over gap
        (1000, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Ramp}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Train}
                    }
                },
                {400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {600, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {800, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Train}
                    }
                },
            }  
        ),
        // Ramps, then slide obstacle
        (650, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Ramp}, {1, (int)ObstacleType.Ramp}, {2, (int)ObstacleType.Ramp}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Train}
                    }
                },
                {500, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.SlideObstacle}, {1, (int)ObstacleType.SlideObstacle}, {2, (int)ObstacleType.SlideObstacle}
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
                        {0, (int)ObstacleType.JumpObstacle}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                 },
                 {200, new Dictionary<int, int>()
                     {
                         {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                     }
                 },
                 {250, new Dictionary<int, int>()
                     {
                         {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Train}
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

        (500, new Dictionary<int, Dictionary<int, int>>()
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
                {300, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.SlideObstacle}, {2, (int)ObstacleType.None}
                    }
                },
                {360, new Dictionary<int, int>()
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
        //
        (1500, new Dictionary<int, Dictionary<int, int>>()
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
                {300, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.SlideObstacle}, {2, (int)ObstacleType.None}
                    }
                },
                {360, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.Train}
                    }
                },
                
            }  
        ),
        (1000, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Ramp}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {210, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.CoinUpper}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {250, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.CoinUpper}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {290, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.CoinUpper}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {410, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.CoinUpper}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {450, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.CoinUpper}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {490, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.CoinUpper}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                
            }  
        ),
    };

	public List<(int, Dictionary<int, Dictionary<int, int>>)> testObstacles = new List<(int, Dictionary<int, Dictionary<int, int>>)>()
    {
        (10, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Coin}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
            }  
        ),
        // "Tunnel" with extra obstacles on the way (switched compared to training)
        (650, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {100, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.JumpObstacle}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {300, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.SlideObstacle}
                    }
                },
                {400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {500, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {600, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
            }
        ),
        // Only a ramp out, can not jump over gap
        (1000, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Ramp}, {1, (int)ObstacleType.SlideObstacle}, {2, (int)ObstacleType.Train}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Train}
                    }
                },
                {400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {600, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {800, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Train}
                    }
                },
            }  
        ),
        // "Tunnel" mit Loch in der Mitte
        (1900, new Dictionary<int, Dictionary<int, int>>()
            {
                {0, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.SlideObstacle}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.Train}
                    }
                },
                {200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {600, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {800, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {1000, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.None}, {2, (int)ObstacleType.None}
                    }
                },
                {1200, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {1400, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.None}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
                {1600, new Dictionary<int, int>()
                    {
                        {0, (int)ObstacleType.Train}, {1, (int)ObstacleType.Train}, {2, (int)ObstacleType.None}
                    }
                },
            }
        ),
	};

    private int testIdx = 0;
	public (int, Dictionary<int, Dictionary<int, int>>) getTestObstacle() {
		testIdx %= obstacles.Count + testObstacles.Count;
        return testIdx < obstacles.Count ? obstacles[testIdx++] : testObstacles[testIdx++];
    }

    public (int, Dictionary<int, Dictionary<int, int>>) getSpecialObstacle()
    {
        return (250, new Dictionary<int, Dictionary<int, int>>()
                {
                    {
                        0, new Dictionary<int, int>()
                        {
                            { 0, (int)ObstacleType.Train }, { 1, (int)ObstacleType.Train }, { 2, (int)ObstacleType.Train }
                        }
                    },
                }
            );
    }
}
