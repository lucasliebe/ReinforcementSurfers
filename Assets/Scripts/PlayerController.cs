using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GroundController groundController;
    private int desiredLane = 1; // 0 = left, higher = right
    private int currentLane = 1;
    private bool isCollided = false;
    private int score = 0;

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
        score = 0;
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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ReSharper disable twice Unity.PerformanceCriticalCodeInvocation
            groundController.Cleanup();
            groundController.Setup();
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
    
    public void SetDesiredLane(int lane)
    {
        desiredLane = lane;
    }

    public Tuple<bool, int> GetState()
    {
        Tuple<bool, int> state = new Tuple<bool, int>(isCollided, score);
        score = 0;
        return state;
    }

    public int GetScore()
    {
        return score;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isCollided && collision.gameObject.CompareTag("Coin"))
        {
            score++;
            Destroy(collision.gameObject);
        }
        else if (!isCollided && collision.gameObject.CompareTag("Obstacle"))
        {
			Debug.Log("Game Over!");
            isCollided = true;
            // UnityEditor.EditorApplication.isPlaying = false;
            // Application.Quit();
        }
	}
}