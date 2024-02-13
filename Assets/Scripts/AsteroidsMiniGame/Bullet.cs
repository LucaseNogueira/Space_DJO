using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5f;

    public int pointsValue = 10;

    public GameObject asteroidPrefab;

    void Start()
    {
        Destroy(gameObject, 1.4f);
    }

    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            FindObjectOfType<AsteroidGameManager>().AddScore(pointsValue);
            other.gameObject.SetActive(false);
            Destroy(gameObject);

            FindObjectOfType<AsteroidManager>().AsteroidDestroyed();
        }
    }
}
