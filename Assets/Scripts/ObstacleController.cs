using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float speed = 1f;
    private float speedIncrement = 0.0f;
    private float deletionCoordinateZ = -20f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate() {
        speed += speedIncrement;
        transform.localPosition += new Vector3(0, 0, -speed);
        if (transform.localPosition.z < deletionCoordinateZ)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetSpeedIncrement(float speedIncrement)
    {
        this.speedIncrement = speedIncrement;
    }
}
