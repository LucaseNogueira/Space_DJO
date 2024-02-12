using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int maxAsteroids = 10;
    private int currentAsteroids = 0;

    void Start()
    {
        for (int i = 0; i < maxAsteroids; i++)
        {
            AddAsteroid();
        }
    }

    public void AddAsteroid()
    {
        if (currentAsteroids < maxAsteroids)
        {
            Instantiate(asteroidPrefab, GenerateRandomPosition(), Quaternion.identity);
            currentAsteroids++;
        }
    }

    public void AsteroidDestroyed()
    {
        currentAsteroids--;
        AddAsteroid();
    }

    private Vector3 GenerateRandomPosition()
    {
        return new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
    }

    public int getCurrentAsteroids()
    {
        return currentAsteroids;
    }
}
