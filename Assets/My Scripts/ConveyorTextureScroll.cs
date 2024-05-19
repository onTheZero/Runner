using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorTextureScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;  // Speed of the texture scrolling
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();  // Get the Renderer component of the GameObject
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;  // Calculate the offset based on time and speed
        rend.material.mainTextureOffset = new Vector2(0, -offset);  // Apply the offset to the material
    }
}
