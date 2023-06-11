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

    private bool isMoving = false;
    private bool isJumping = false;
    private bool isSliding = false;
    private float[] movingTargetXs = {-3f, 0, 3f};
    private float slidingTargetY = 0.5f;
    private float jumpingTargetY = 4f;
    private float defaultTargetY = 1.1f;

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

    private Vector3 CalculateTargetPosition()
    {
        Vector3 targetPosition = transform.localPosition;

        if (isMoving)
        {
            targetPosition.x = movingTargetXs[desiredLane];
        }
        if (isJumping)
        {
            targetPosition.y = jumpingTargetY;
        }
        if (isSliding)
        {
            targetPosition.y = slidingTargetY;
        }
        else if (!isJumping && !isSliding)
        {
            targetPosition.y = defaultTargetY;
        }

        return targetPosition;
    }

    private Vector3 CalculateTargetRotation()
    {
        return (isSliding) ? new Vector3(90, 0, 0) : new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            MoveLane();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            MoveLane();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TriggerIsJumping();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TriggerIsSliding();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ReSharper disable twice Unity.PerformanceCriticalCodeInvocation
            groundController.Cleanup();
            groundController.Setup();
        }
    }

    void FixedUpdate() 
    {
        Vector3 targetPosition = CalculateTargetPosition();
        Vector3 targetRotation = CalculateTargetRotation();

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.15f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), 0.15f);
        // "Snap" position and rotation to prevent endless linear interpolation
        if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f)
        {
            transform.localPosition = targetPosition;
            isMoving = false;
            isSliding = false;
        }
        if (Vector3.Distance(transform.localRotation.eulerAngles, targetRotation) < 0.2f)
        {
            transform.localRotation = Quaternion.Euler(targetRotation);
        }
        if (jumpingTargetY - transform.localPosition.y < 0.1f)
        {
            isJumping = false;
        }
    }
    
    public void MoveLane()
    {
        isMoving = true;
        if (desiredLane < 0 || desiredLane > groundController.lanes - 1)
            isCollided = true;
        desiredLane = Math.Clamp(desiredLane, 0, groundController.lanes - 1);
        currentLane = desiredLane;
    }
    
    public int GetCurrentLane()
    {
        return currentLane;
    }

    public void SetDesiredLane(int lane)
    {
        desiredLane = lane;
    }

    public void TriggerIsJumping()
    {
        if (transform.position.y <= 1.1f) isJumping = true;
    }

    public void TriggerIsSliding()
    {
        if (transform.rotation.x <= 0) 
        {
            isSliding = true;
            isJumping = false;
        }
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
        else if (!isCollided && 
            (collision.gameObject.CompareTag("Obstacle") || 
            collision.gameObject.CompareTag("JumpObstacle") || 
            collision.gameObject.CompareTag("SlideObstacle")))
        {
			Debug.Log("Game Over!");
            score = -1;
            isCollided = true;
            // UnityEditor.EditorApplication.isPlaying = false;
            // Application.Quit();
        }
	}
}