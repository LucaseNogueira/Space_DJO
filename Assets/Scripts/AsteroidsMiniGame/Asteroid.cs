using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rb;

    public float movementSpeed = 50f;
    public float rotationSpeed = 200f;

    float rotationDir;
    float rotation;

    Vector2 direction;

    void Start()
    {
        rotationDir = Random.value > 0.5f ? -1f : 1f;
        direction = (Vector3.zero - transform.position).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = movementSpeed * direction;
    }

    void Update()
    {
        rotation += rotationDir * Time.smoothDeltaTime * rotationSpeed;

    }
}
