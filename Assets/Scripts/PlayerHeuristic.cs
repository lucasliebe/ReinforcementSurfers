using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeuristic : MonoBehaviour
{
    public bool IsEnabled = true;
    private PlayerController _playerController;
    private float[] spawnerPositionsX = new float[3];

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

    private void MoveToLane(int lane)
    {
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
        
        // Get closest object on each lane
        GameObject closestObjectOnLane0 = GetClosestGameObjectOnLane(0);
        GameObject closestObjectOnLane1 = GetClosestGameObjectOnLane(1);
        GameObject closestObjectOnLane2 = GetClosestGameObjectOnLane(2);

        // Get closest object overall
        GameObject closestObject = GetClosestObject(
            closestObjectOnLane0,
            closestObjectOnLane1,
            closestObjectOnLane2
        );

        // Get second closest object overall
        GameObject secondClosestObject = GetClosestObject(
            closestObjectOnLane0 == closestObject ? null : closestObjectOnLane0,
            closestObjectOnLane1 == closestObject ? null : closestObjectOnLane1,
            closestObjectOnLane2 == closestObject ? null : closestObjectOnLane2
        );

        // Determine lanes of closest and second closest object and move to the third lane
        if (closestObject != null && secondClosestObject != null)
        {
            int closestObjectLane = GetObjectsLane(closestObject);
            int secondClosestObjectLane = GetObjectsLane(secondClosestObject);

            List<int> lanes = new List<int> {0, 1, 2};
            lanes.Remove(closestObjectLane);
            lanes.Remove(secondClosestObjectLane);

            int thirdLane = lanes[0];
            MoveToLane(thirdLane);

            // TODO:
            // Jump and slide if necessary (Need distance to object --> function already exists)
            // Collect Coins
            // Move lane after objects have passed
        }
        
        
    }
}
