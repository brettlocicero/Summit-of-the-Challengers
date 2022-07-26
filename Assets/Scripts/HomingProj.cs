using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProj : MonoBehaviour
{
    [SerializeField] Transform target;

	[SerializeField] float speed = 5f;
	[SerializeField] float rotateSpeed = 200f;

    Rigidbody2D rb;

    void Start()
    {
        target = PlayerInstance.instance.transform;
    }

    void FixedUpdate ()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.velocity = transform.up * speed;
    }
}
