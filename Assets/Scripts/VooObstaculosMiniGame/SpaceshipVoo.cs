using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipVoo : MonoBehaviour
{
    public float speed = 15.0f;
    public float liftSpeed = 5.0f;
    public float descentSpeed = 3.0f;
    public float rotationSpeed = 100.0f;

    public int live = 100;

    private bool isAscending = false;
    private float originalSpeed;

    void Start()
    {
        originalSpeed = speed;
    }

    void FixedUpdate()
    {
        // Movimento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

        // Movimento vertical
        if (Input.GetKey(KeyCode.Space))
        {
            isAscending = true;
            transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);
        }
        else
        {
            isAscending = false;
        }

        // Descida gradual quando não estiver subindo
        if (!isAscending)
        {
            transform.Translate(Vector3.down * descentSpeed * Time.deltaTime);
        }

        // Rotação
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void IncreaseSpeed(float amount)
    {
        StopCoroutine("ResetSpeed");

        speed += amount;

        StartCoroutine("ResetSpeed");
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(2);
        speed = originalSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo")
        {
            live -= 10;

            if (live <= 0)
            {
                GameControllerVoo.Instance.RestartGame();
            }
        }
    }
}
