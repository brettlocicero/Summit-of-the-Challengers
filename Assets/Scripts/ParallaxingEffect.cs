using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxingEffect : MonoBehaviour
{
    [SerializeField] float moveAmount;
    [SerializeField] Transform cam;
    [SerializeField] Vector3 offset;
    float moveDirection;

    void Update()
    {
        float dx = cam.position.x * moveAmount;
        float dy = cam.position.y * moveAmount;
        Vector3 newPosition = new Vector3(cam.position.x - dx, cam.position.y - dy, transform.position.z) - offset;
        transform.position = newPosition;
    }
}
