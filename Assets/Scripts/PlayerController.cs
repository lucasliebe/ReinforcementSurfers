using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isCollided = false;
    
    private GroundController groundController;
    public int desiredLane = 1; // 0 = left, higher = right

    private int currentLane = 1;

    // Start is called before the first frame update
    void Start()
    {
        groundController = GameObject.Find("Ground").GetComponent<GroundController>();
    }

    public void Reset()
    {
        isCollided = false;
        desiredLane = 1;
        currentLane = 1;
        transform.localPosition = new Vector3(0, 1.1f, -6);
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
    
    public void MoveLane()
    {
        desiredLane = Math.Clamp(desiredLane, 0, groundController.lanes - 1);
        int change = desiredLane - currentLane;
        currentLane = desiredLane;
        transform.localPosition += Vector3.right * (change * groundController.getLaneDistance());
    }

	void OnCollisionEnter(Collision collision)
    {
        if (!isCollided && collision.gameObject.CompareTag("Obstacle"))
        {
			Debug.Log("Game Over!");
            isCollided = true;
            // UnityEditor.EditorApplication.isPlaying = false;
            // Application.Quit();
        }
	}
}