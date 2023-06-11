using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashcanController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || 
        collision.gameObject.tag == "Coin" || 
        collision.gameObject.tag == "JumpObstacle" ||
        collision.gameObject.tag == "SlideObstacle"
        )
        {
            Destroy(collision.gameObject);
        }
    }
}
