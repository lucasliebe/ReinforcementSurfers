using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition += new Vector3(0,0,-speed);
    }
    
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
