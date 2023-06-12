using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{

    public float ScrollSpeedX = 0; 
    public float ScrollSpeedY = 0;
    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(ScrollSpeedX, ScrollSpeedY) * Time.deltaTime;
    }
}
