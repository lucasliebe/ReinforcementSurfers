using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GroundController groundController;
    private int desiredLane = 0; // 0 = left, higher = right

    private int currentLane = 0;

    // Start is called before the first frame update
    void Start()
    {
        groundController = GameObject.Find("Ground").GetComponent<GroundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
        }
        MoveLane();
    }
    
    private void MoveLane()
    {
        Vector3 targetPosition = transform.position;
        int change = desiredLane - currentLane;
        if (change == 1)
        {
            if (desiredLane > groundController.lanes - 1)
            {
                desiredLane = groundController.lanes - 1;
            }

            else
            {
                targetPosition += Vector3.right * groundController.getLaneDistance();
                currentLane++;
            }
        }
        else if (change == -1)
        {
            if (desiredLane < 0)
            {
                desiredLane = 0;
            }
            else
            {
                targetPosition += Vector3.left * groundController.getLaneDistance();
                currentLane--;
            }
        }
        transform.position = targetPosition;
    }
}