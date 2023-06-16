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
        return obj == null ? 1000.0f : obj.transform.position.z + 6.0f;
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

    private void MoveLeft()
    {
        _playerController.SetDesiredLane(_playerController.GetCurrentLane() - 1);
        _playerController.MoveLane();
    }

    private void MoveRight()
    {
        _playerController.SetDesiredLane(_playerController.GetCurrentLane() + 1);
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

    private int GetObjectsLane(GameObject obj)
    {
        int lane = -1;
        if (obj.transform.position.x == spawnerPositionsX[0]) lane = 0;
        if (obj.transform.position.x == spawnerPositionsX[1]) lane = 1;
        if (obj.transform.position.x == spawnerPositionsX[2]) lane = 2;

        return lane;
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

        // Dodge obstacles
        
        
    }
}
