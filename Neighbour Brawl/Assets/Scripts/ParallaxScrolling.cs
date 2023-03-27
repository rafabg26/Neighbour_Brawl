using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    private float width, startPosition;
    private Transform cam;
    public float parallaxFraction;
    void Start() {
        startPosition = transform.position.x;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }
    void LateUpdate() {
        float offset = (cam.position.x * parallaxFraction);
        float moved = cam.position.x - offset;
        if (moved > startPosition + width) startPosition += width;
        else if (moved < startPosition - width) startPosition -= width;
        transform.position = new Vector3(startPosition + offset,
        transform.position.y,
        transform.position.z);
    }

}
