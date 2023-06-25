using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float speed = 1f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.localPosition + new Vector3(0,0,-speed));
        //rb.AddForce(Vector3.forward * -speed, ForceMode.VelocityChange);
    }
    
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
