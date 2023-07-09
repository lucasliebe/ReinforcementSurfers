using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public TMP_Text scoreText;
    public GameObject multiplierIcon;
    public GameObject shieldIcon;

    private GroundController groundController;
    private Rigidbody rb;
    private Material material;

    private int desiredLane = 1; // 0 = left, higher = right
    private int currentLane = 1;
    private int score = 0;
    private int multiplier = 1;
    private float total_score = 0f;
    private float[] movingTargetXs = { -3f, 0, 3f };

    private bool isCollided = false;
    private bool isMoving = false;
    private bool isSliding = false;
    public bool isFrozen = false;

    private bool isJumping {
        get => jumpCoroutine != null;
    }

    private bool isShielded = false;

    public bool canMultiply = true;
    public bool canShield = true;

    private Coroutine multiplierCoroutine;
    private Coroutine shieldCoroutine;
    private Coroutine jumpCoroutine;

    // Start is called before the first frame update
    void Start() {
        groundController = GameObject.Find("Ground").GetComponent<GroundController>();
        rb = GetComponent<Rigidbody>();
        material = GetComponent<Renderer>().material;
        material.color = new Color(0, 0, 0);
    }

    public void Reset() {
        StopAllCoroutines();
        jumpCoroutine = null;
        multiplierCoroutine = null;
        shieldCoroutine = null;
        canMultiply = true;
        multiplierIcon.SetActive(true);
        canShield = true;
        shieldIcon.SetActive(true);
        rb.velocity = Vector3.zero;
        isCollided = false;
        isMoving = false;
        isSliding = false;
        isShielded = false;
        desiredLane = 1;
        currentLane = 1;
        score = 0;
        total_score = 0;
        multiplier = 1;
        transform.localPosition = new Vector3(0, 6.0f, -6);
        material.color = new Color(0, 0, 0);
    }

    private Vector3 CalculateTargetPosition() {
        Vector3 targetPosition = transform.localPosition;

        if (isMoving) {
            targetPosition.x = movingTargetXs[desiredLane];
        }

        targetPosition.y += 0.1f;

        return targetPosition;
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.localPosition;
    }

    private Vector3 CalculateTargetRotation() { return (isSliding) ? new Vector3(90, 0, 0) : new Vector3(0, 0, 0); }

    void Update() {
        scoreText.text = "Score: " + MathF.Round(total_score * 1000f) / 1000f;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            desiredLane--;
            MoveLane();
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            desiredLane++;
            MoveLane();
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            TriggerIsJumping();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            TriggerIsSliding();
        } else if (Input.GetKeyDown(KeyCode.M)) {
            TriggerMultiplier();
        } else if (Input.GetKeyDown(KeyCode.S)) {
            TriggerShield();
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            // ReSharper disable twice Unity.PerformanceCriticalCodeInvocation
            groundController.Cleanup();
            groundController.Setup();
        }
    }

    void FixedUpdate() {
        Vector3 targetPosition = CalculateTargetPosition();
        Vector3 targetRotation = CalculateTargetRotation();

        if (transform.localPosition.y < 0) {
            transform.localPosition =
                Vector3.Lerp(transform.localPosition, targetPosition, 0.15f) + new Vector3(0, 1.2f, 0);
        } else {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.15f);
        }

        //rb.MovePosition(Vector3.Lerp(transform.localPosition, targetPosition, 0.15f));
        //rb.AddForce((targetPosition - transform.localPosition) * 0.2f, ForceMode.Impulse);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), 0.5f);
        // "Snap" position and rotation to prevent endless linear interpolation
        if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f) {
            //transform.localPosition = targetPosition;
            rb.MovePosition(targetPosition);
            isMoving = false;
        }

        if (Vector3.Distance(transform.localRotation.eulerAngles, targetRotation) < 0.1f) {
            transform.localRotation = Quaternion.Euler(targetRotation);
            isSliding = false;
        }

        rb.AddForce(Physics.gravity * 3f, ForceMode.Acceleration);
        total_score += 0.0005f * multiplier;
    }

    public void MoveLane() {
        isMoving = true;
        // if (desiredLane < 0 || desiredLane > groundController.lanes - 1)
        //    isCollided = true;
        desiredLane = Math.Clamp(desiredLane, 0, groundController.lanes - 1);
        currentLane = desiredLane;
    }

    public int GetCurrentLane() { return currentLane; }

    public void SetDesiredLane(int lane) { desiredLane = lane; }

    public void TriggerIsJumping() {
        if (!isJumping) {
            rb.AddForce(Physics.gravity * -1.5f, ForceMode.Impulse);
            jumpCoroutine = StartCoroutine(EndJumping(0.7f));
        }
    }

    private IEnumerator EndJumping(float wait) {
        yield return new WaitForSecondsRealtime(wait);
        jumpCoroutine = null;
    }

    public void TriggerIsSliding() {
        if (transform.rotation.x <= 0) {
            rb.AddForce(Physics.gravity * 2f, ForceMode.Impulse);
            isSliding = true;
        }
    }

    public void TriggerMultiplier() {
        if (canMultiply) {
            multiplier = 2;
            material.color += new Color(0, 255, 0);
            canMultiply = false;
            multiplierIcon.SetActive(false);
            multiplierCoroutine = StartCoroutine(EndMultiplier());
        }
    }

    private IEnumerator EndMultiplier() {
        yield return new WaitForSecondsRealtime(3);
        multiplier = 1;
        material.color -= new Color(0, 255, 0);
        yield return new WaitForSecondsRealtime(6);
        canMultiply = true;
        multiplierIcon.SetActive(true);
    }

    public void TriggerShield() {
        if (canShield) {
            isShielded = true;
            material.color += new Color(0, 0, 255);
            canShield = false;
            shieldIcon.SetActive(false);
            shieldCoroutine = StartCoroutine(EndShield());
        }
    }

    public void ForceShieldChange() { }

    private IEnumerator EndShield() {
        yield return new WaitForSecondsRealtime(1.5f);
        isShielded = false;
        material.color -= new Color(0, 0, 255);
        yield return new WaitForSecondsRealtime(10);
        canShield = true;
        shieldIcon.SetActive(true);
    }

    public Tuple<bool, int, int> GetState() {
        if (isFrozen) {
            return new Tuple<bool, int, int>(false, 0, 0);
        }

        Tuple<bool, int, int> state = new Tuple<bool, int, int>(isCollided, score, multiplier);
        score = 0;
        return state;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ramp")) {
            Vector3 normal = collision.contacts[0].normal;

            if (normal != new Vector3(0, 1, 0)) Die();
            transform.localPosition = new Vector3(transform.localPosition.x, 4f, transform.localPosition.z);
        }

        if (isShielded || isFrozen) return;

        if (!isCollided && collision.gameObject.CompareTag("Coin")) {
            score++;
            total_score += 0.4f * multiplier;
            Destroy(collision.gameObject);
        } else if (!isCollided &&
                   (collision.gameObject.CompareTag("Obstacle") ||
                    collision.gameObject.CompareTag("JumpObstacle") ||
                    collision.gameObject.CompareTag("SlideObstacle"))) {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Game Over! Total Score: " + MathF.Round(total_score * 1000f) / 1000f);
        score = 0;
        isCollided = true;
    }
}