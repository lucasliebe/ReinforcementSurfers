using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeuristic : MonoBehaviour
{
    public bool IsEnabled = true;
    private PlayerController _playerController;
    private float[] spawnerPositionsX = new float[3];

    private GameObject closestObject;
    private GameObject secondClosestObject;
    private GameObject player;

    private void InitializeSpawnerPositionsX()
    {
        // Does not work (all 0)
        // var spawnAreas = GameObject.FindGameObjectsWithTag("SpawnArea");
        // for (int i = 0; i < spawnAreas.Length; i++)
        // {
        //     spawnerPositionsX[i] = spawnAreas[i].transform.position.x;
        // }

        // System.Array.Sort(spawnerPositionsX);

        // Hardcode for now
        spawnerPositionsX[0] = -3.0f;
        spawnerPositionsX[1] = 0.0f;
        spawnerPositionsX[2] = 3.0f;
    }
    
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        InitializeSpawnerPositionsX();

        player = GameObject.Find("Player");
    }

    private int GetActualPosition()
    {
        if (player.transform.position.x <= -1.5f) return 0;
        if (player.transform.position.x >= 1.5f) return 2;
        return 1;
    }

    private List<GameObject> GetGameObjectsWithTagOnLane(string tag, int lane)
    {
        var objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        var objectsOnLane = new List<GameObject>();

        foreach (var obj in objectsWithTag)
        {
            // Object is behind player
            if (obj.transform.position.z < player.transform.position.z) continue;

            if (obj.transform.position.x == spawnerPositionsX[lane])
            {
                objectsOnLane.Add(obj);
            }
        }

        return objectsOnLane;
    }


    private List<GameObject> GetAllGameObjectsOnLane(int lane)
    {
        List<GameObject> objectsOnLane = new List<GameObject>();
        objectsOnLane.AddRange(GetGameObjectsWithTagOnLane("Obstacle", lane));
        objectsOnLane.AddRange(GetGameObjectsWithTagOnLane("Coin", lane));
        objectsOnLane.AddRange(GetGameObjectsWithTagOnLane("JumpObstacle", lane));
        objectsOnLane.AddRange(GetGameObjectsWithTagOnLane("SlideObstacle", lane));

        return objectsOnLane;
    }
    
    private GameObject GetClosestGameObjectOnLane(int lane)
    {
        List<GameObject> closestObjects = GetAllGameObjectsOnLane(lane);
        GameObject closestObject = null;

        foreach (var obj in closestObjects)
        {
            if (closestObject == null)
            {
                closestObject = obj;
            }
            else
            {
                if (obj.transform.position.z < closestObject.transform.position.z)
                {
                    closestObject = obj;
                }
            }
        }

        return closestObject;
    }

    private float GetDistanceToObject(GameObject obj)
    {
        return obj == null 
            ? 1000.0f 
            : (obj.transform.position.z >= player.transform.position.z) 
                ? obj.transform.position.z - player.transform.position.z
                : 1000.0f; // Object is behind player
    }

    private int GetObjectsLane(GameObject obj)
    {
        int lane = -1;
        if (obj.transform.position.x == spawnerPositionsX[0]) lane = 0;
        else if (obj.transform.position.x == spawnerPositionsX[1]) lane = 1;
        else if (obj.transform.position.x == spawnerPositionsX[2]) lane = 2;

        return lane;
    }

    private GameObject GetClosestObject(GameObject object0, GameObject object1, GameObject object2)
    {
        GameObject closestObject = null;
        float closestDistance = 1000.0f;

        if (GetDistanceToObject(object0) < closestDistance)
        {
            closestObject = object0;
            closestDistance = GetDistanceToObject(object0);
        }
        if (GetDistanceToObject(object1) < closestDistance)
        {
            closestObject = object1;
            closestDistance = GetDistanceToObject(object1);
        }
        if (GetDistanceToObject(object2) < closestDistance)
        {
            closestObject = object2;
            closestDistance = GetDistanceToObject(object2);
        }

        return closestObject;

    }

    // <summary>
    // Returns a list of obstacles that the player would collide with if it moved to the given lane
    // </summary>
    private bool BlockingObstacleExistsOnLane(int lane)
    {
        var obstacles = new List<GameObject>();
        obstacles.AddRange(GameObject.FindGameObjectsWithTag("Obstacle"));
        obstacles.AddRange(GameObject.FindGameObjectsWithTag("JumpObstacle"));
        obstacles.AddRange(GameObject.FindGameObjectsWithTag("SlideObstacle"));

        foreach (var obj in obstacles)
        {
            // Hardcoded distances to obstacles for now
            if ((obj.transform.position.z < -12.0f || obj.transform.position.z > 1f) && obj.tag == "Obstacle") 
                continue;
            if ((obj.transform.position.z < -8.0f || obj.transform.position.z > -3f) && obj.tag == "JumpObstacle")
                continue;
            if ((obj.transform.position.z < -8.0f || obj.transform.position.z > -3f) && obj.tag == "SlideObstacle")
                continue;

            if (obj.transform.position.x == spawnerPositionsX[lane])
            {
                return true;
            }
        }

        return false;
    }

    private bool CanMoveToLane(int lane)
    {
        int currentLane = _playerController.GetCurrentLane();
        if (currentLane == lane) return true;
        if (currentLane == 1 && lane == 2)
        {
            return !BlockingObstacleExistsOnLane(2);
        }
        if (currentLane == 1 && lane == 0)
        {
            return !BlockingObstacleExistsOnLane(0);
        }
        // else 1
        if (currentLane == 0 && lane == 1) 
        {
            return !BlockingObstacleExistsOnLane(1);
        }
        if (currentLane == 2 && lane == 1)
        {
            return !BlockingObstacleExistsOnLane(1);
        }
        if (currentLane == 0 && lane == 2)
        {
            return !BlockingObstacleExistsOnLane(1);
        }
        if (currentLane == 2 && lane == 0)
        {
            return !BlockingObstacleExistsOnLane(1);
        }

        return true;
    }

    private bool MoveToLane(int lane)
    {
        if (!CanMoveToLane(lane)) return false;
        _playerController.SetDesiredLane(lane);
        _playerController.MoveLane();
        return true;
    }

    private void Jump()
    {
        _playerController.TriggerIsJumping();
    }

    private void Slide()
    {
        _playerController.TriggerIsSliding();
    }

    void FixedUpdate()
    {
        if (!IsEnabled) return;
        
        // Get closest object on each lane
        GameObject closestObjectOnLane0 = GetClosestGameObjectOnLane(0);
        GameObject closestObjectOnLane1 = GetClosestGameObjectOnLane(1);
        GameObject closestObjectOnLane2 = GetClosestGameObjectOnLane(2);
        GameObject incomingObject = GetClosestGameObjectOnLane(GetActualPosition());

        // Get closest object overall
        closestObject = GetClosestObject(
            closestObjectOnLane0,
            closestObjectOnLane1,
            closestObjectOnLane2
        );

        // Get second closest object overall
        secondClosestObject = GetClosestObject(
            closestObjectOnLane0 == closestObject ? null : closestObjectOnLane0,
            closestObjectOnLane1 == closestObject ? null : closestObjectOnLane1,
            closestObjectOnLane2 == closestObject ? null : closestObjectOnLane2
        );

        if (closestObject == null || secondClosestObject == null || incomingObject == null) return;
        
        int closestObjectLane = GetObjectsLane(closestObject);
        int secondClosestObjectLane = GetObjectsLane(secondClosestObject);

        // Move to coin's lane (if possible)
        if (closestObject.tag == "Coin")
        {
            if (MoveToLane(closestObjectLane)) return;
        }
        if (secondClosestObject.tag == "Coin")
        {
            if (MoveToLane(secondClosestObjectLane)) return;
        }
        
        // Dodge obstacle if its a train
        if (incomingObject.tag == "Obstacle") {
            List<int> lanes = new List<int> {0, 1, 2};
            lanes.Remove(closestObjectLane);
            lanes.Remove(secondClosestObjectLane);

            int thirdLane = lanes[0];

            // Prefer a lane to dodge to that has a jump or slide obstacle (only if its still far enough away)
            if ((secondClosestObject.tag == "SlideObstacle" || secondClosestObject.tag == "JumpObstacle")
                && GetDistanceToObject(secondClosestObject) >= 4.5f)
            {
                thirdLane = secondClosestObjectLane;
            }

            if (MoveToLane(thirdLane)) return;
        }

        // If it's not a train, jump or slide accordingly
        if (incomingObject.tag == "JumpObstacle" || incomingObject.tag == "SlideObstacle")
        {
            if (GetDistanceToObject(incomingObject) <= 3.5f)
            {
                if (incomingObject.tag == "JumpObstacle")
                {
                    Jump();
                }
                else if (incomingObject.tag == "SlideObstacle")
                {
                    Slide();
                }
            }
        }
    }

}
