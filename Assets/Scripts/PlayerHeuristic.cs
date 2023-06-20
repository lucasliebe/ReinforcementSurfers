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
    private bool idle = false;

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
    }

    private List<GameObject> GetGameObjectsWithTagOnLane(string tag, int lane)
    {
        var objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        var objectsOnLane = new List<GameObject>();

        foreach (var obj in objectsWithTag)
        {
            // Object is behind player
            if (obj.transform.position.z < -6.0f) continue;

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
        objectsOnLane.AddRange(GetGameObjectsWithTagOnLane("JumpObstacle", lane));
        objectsOnLane.AddRange(GetGameObjectsWithTagOnLane("SlideObstacle", lane));
        objectsOnLane.AddRange(GetGameObjectsWithTagOnLane("Coin", lane));

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
        return obj == null ? 1000.0f : Mathf.Abs(obj.transform.position.z) - 6.0f;
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

    private List<GameObject> GetBlockingObstacles(List<int> lanes)
    {
        var trucks = GameObject.FindGameObjectsWithTag("Obstacle");
        var objectsOnLane = new List<GameObject>();

        foreach (var obj in trucks)
        {
            if (obj.transform.position.z < -16.0f && obj.transform.position.z > 6.2f) continue;

            foreach (int lane in lanes)
            {
                if (obj.transform.position.x == spawnerPositionsX[lane])
                {
                    objectsOnLane.Add(obj);
                }
            }
        }

        return objectsOnLane;
    }

    private bool CanMoveToLane(int lane)
    {
        int currentLane = _playerController.GetCurrentLane();
        if (currentLane == lane) return true;
        if (currentLane == 0 && lane == 1) 
        {
            var blockingObstacles = GetBlockingObstacles(new List<int> {1});
            if (blockingObstacles.Count > 0) return false;
        }
        if (currentLane == 1 && lane == 0)
        {
            var blockingObstacles = GetBlockingObstacles(new List<int> {0});
            if (blockingObstacles.Count > 0) return false;
        }
        if (currentLane == 1 && lane == 2)
        {
            var blockingObstacles = GetBlockingObstacles(new List<int> {2});
            if (blockingObstacles.Count > 0) return false;
        }
        if (currentLane == 2 && lane == 1)
        {
            var blockingObstacles = GetBlockingObstacles(new List<int> {1});
            if (blockingObstacles.Count > 0) return false;
        }
        if (currentLane == 0 && lane == 2)
        {
            var blockingObstacles = GetBlockingObstacles(new List<int> {1});
            if (blockingObstacles.Count > 0) return false;
        }
        if (currentLane == 2 && lane == 0)
        {
            var blockingObstacles = GetBlockingObstacles(new List<int> {1});
            if (blockingObstacles.Count > 0) return false;
        }

         return true;
    }

    private void MoveToLane(int lane)
    {
        if (!CanMoveToLane(lane)) return;
        _playerController.SetDesiredLane(lane);
        _playerController.MoveLane();
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
        
        if (!idle)
        {
            // Get closest object on each lane
            GameObject closestObjectOnLane0 = GetClosestGameObjectOnLane(0);
            GameObject closestObjectOnLane1 = GetClosestGameObjectOnLane(1);
            GameObject closestObjectOnLane2 = GetClosestGameObjectOnLane(2);

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
        }

        if (closestObject == null || secondClosestObject == null) return;
        
        int closestObjectLane = GetObjectsLane(closestObject);
        int secondClosestObjectLane = GetObjectsLane(secondClosestObject);

        // Move to coin's lane
        if (closestObject.tag == "Coin")
        {
            MoveToLane(closestObjectLane);
        }
        else if (secondClosestObject.tag == "Coin")
        {
            MoveToLane(secondClosestObjectLane);
        }
        // Move to Jump/Slide Obstacle's lane, wait, then jump/slide
        else if (closestObject.tag == "JumpObstacle" || closestObject.tag == "SlideObstacle")
        {
            idle = true;
            MoveToLane(GetObjectsLane(closestObject));
            if (GetDistanceToObject(closestObject) <= 1.0f)
            {
                if (closestObject.tag == "JumpObstacle")
                {
                    Jump();
                }
                else if (closestObject.tag == "SlideObstacle")
                {
                    Slide();
                }
                idle = false;
            }
            return;
        }
        // Move to lane where the next obstacle is farthest away
        else 
        {
            List<int> lanes = new List<int> {0, 1, 2};
            lanes.Remove(closestObjectLane);
            lanes.Remove(secondClosestObjectLane);

            int thirdLane = lanes[0];
            MoveToLane(thirdLane);
        }
        
        // TODO:
        // Problem: player.CurrentLane is set to a lane while the player is still moving to that lane
        
        
    }
}
