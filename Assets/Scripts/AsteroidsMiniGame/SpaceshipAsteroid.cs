using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipAsteroid : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed = 5f;
    public float rotationSpeed = 160f;

    public GameObject bulletPrefab;

    float rotation;

    public List<AudioClip> efeitoSonoros;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical");

        rb.AddForce(((Vector2)transform.up * v * movementSpeed) - rb.velocity, ForceMode2D.Force);
    }


    void Update()
    {
        float rotationDir = 0f;
        if (Input.GetKey(KeyCode.A))
            rotationDir = 1f;
        else if (Input.GetKey(KeyCode.D))
            rotationDir = -1f;

        rotation += rotationDir * Time.smoothDeltaTime * rotationSpeed;

        transform.localEulerAngles = new Vector3(0f, 0f, rotation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.clip = efeitoSonoros[1];
            audioSource.Play();
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Asteroid")
        {
            audioSource.clip = efeitoSonoros[0];
            audioSource.Play();
            FindObjectOfType<AsteroidGameManager>().LoseLife();
        }
    }
}
