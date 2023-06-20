using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TMP_Text scoreText;
    private GroundController groundController;
    private int desiredLane = 1; // 0 = left, higher = right
    private int currentLane = 1;
    private bool isCollided = false;
    private int score = 0;
    private float total_score = 0f;

    private bool isMoving = false;
    private bool isSliding = false;
    private float[] movingTargetXs = {-3f, 0, 3f};
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        groundController = GameObject.Find("Ground").GetComponent<GroundController>();
        rb = GetComponent<Rigidbody>();
    }

    public void Reset()
    {
        isCollided = false;
        desiredLane = 1;
        currentLane = 1;
        score = 0;
        total_score = 0;
        transform.localPosition = new Vector3(0, 1.1f, -6);
    }

    private Vector3 CalculateTargetPosition()
    {
        Vector3 targetPosition = transform.localPosition;

        if (isMoving)
        {
            targetPosition.x = movingTargetXs[desiredLane];
        }

        return targetPosition;
    }

    private Vector3 CalculateTargetRotation()
    {
        return (isSliding) ? new Vector3(90, 0, 0) : new Vector3(0, 0, 0);
    }

    void Update()
    {
        scoreText.text = "Score: " + MathF.Round(total_score*1000f)/1000f;
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

        //transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.15f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), 0.5f);
        // "Snap" position and rotation to prevent endless linear interpolation
        if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f)
        {
            //transform.localPosition = targetPosition;
            isMoving = false;
        }
        if (Vector3.Distance(transform.localRotation.eulerAngles, targetRotation) < 0.1f)
        {
            transform.localRotation = Quaternion.Euler(targetRotation);
            isSliding = false;
        }
        rb.AddForce(Physics.gravity * 3f, ForceMode.Acceleration);
        total_score += 0.0005f;
    }
    
    public void MoveLane()
    {
        isMoving = true;
        // if (desiredLane < 0 || desiredLane > groundController.lanes - 1)
        //    isCollided = true;
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
        if (transform.position.y <= 1.1f)
        {
            rb.AddForce(Physics.gravity * -3.5f, ForceMode.VelocityChange);
        }
    }

    public void TriggerIsSliding()
    {
        if (transform.rotation.x <= 0) 
        {
            rb.AddForce(Physics.gravity * 3f, ForceMode.VelocityChange);
            isSliding = true;
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
            total_score += 0.5f;
            Destroy(collision.gameObject);
        }
        else if (!isCollided && 
            (collision.gameObject.CompareTag("Obstacle") || 
            collision.gameObject.CompareTag("JumpObstacle") || 
            collision.gameObject.CompareTag("SlideObstacle")))
        {
			Debug.Log("Game Over! Total Score: " + MathF.Round(total_score*1000f)/1000f);
            score = -1;
            isCollided = true;
            // UnityEditor.EditorApplication.isPlaying = false;
            // Application.Quit();
        }
	}
}