using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostTrigger : MonoBehaviour
{
    public float boostAmount = 10.0f;
    public int scoreIncrease = 10;

    public bool isEndGame = false;

    public SpaceshipVoo spaceship;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameControllerVoo.Instance.AddScore(scoreIncrease);

            spaceship.IncreaseSpeed(boostAmount);

            if (isEndGame)
            {
                GameControllerVoo.Instance.EndGame();
            }

            gameObject.SetActive(false);
        }
    }
}
