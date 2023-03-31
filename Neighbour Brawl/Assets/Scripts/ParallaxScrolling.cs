using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    private float width, startPosition;
    private Transform cam;
    public float parallaxFraction;
    private SpriteRenderer spriteRenderer;
    private float offset, moved;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position.x;
        width = spriteRenderer.bounds.size.x;
        cam = Camera.main.transform;
    }
    void Update() {
        offset = (cam.position.x * parallaxFraction);
        moved = cam.position.x - offset;
    }
    void FixedUpdate() {
        if (moved > startPosition + width) startPosition += width;
        else if (moved < startPosition - width) startPosition -= width;
        transform.position = new Vector3(startPosition + offset,
        transform.position.y,
        transform.position.z);
    }

}
