using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsteroidGameManager : MonoBehaviour
{
    public static int score = 0;
    public static int lives = 3;

    public Text scoreText;
    public Text livesText;

    public Restarter restarter;

    public GameObject areaPause;

    void Start()
    {
        UpdateScoreDisplay();
        UpdateLivesDisplay();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Pausar();
        }
        if (FindObjectOfType<AsteroidManager>().getCurrentAsteroids() == 0)
        {
            LoseLife();
        }
    }


    // Metodo para adicionar pontos
    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScoreDisplay();
    }

    public void LoseLife()
    {
        lives--;
        UpdateLivesDisplay();

        if (lives <= 0)
        {
            EndGame();
        }
        else
        {
            restartGame();
        }
    }

    /**
    * Esta função "pausa" ou "despausa" o jogo
    **/
    private void Pausar()
    {
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
        if(Time.timeScale == 0f){
            ShowTelaPause();
        } else
        {
            CloseTelaPause();
        }
    }

    private void ShowTelaPause()
    {
        areaPause.SetActive(true);
        HabilitarCursor();
    }

    private void CloseTelaPause()
    {   
        areaPause.SetActive(false);
        DesabilitarCursor();
    }

    private void HabilitarCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DesabilitarCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Atualizar a exibicao da pontuacao
    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void UpdateLivesDisplay()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    private void EndGame()
    {
        PlayerPrefs.SetInt("AsteroidScore", score);

        PlayerPrefs.SetInt("MiniGameScore", score);
        PlayerPrefs.SetString("ExitSceneToLoad", "Academia");
        PlayerPrefs.SetString("RestartSceneToLoad", "Asteroids");
        PlayerPrefs.SetString("MiniGameLastPlayed", "Destruir Asteroides");

        PlayerPrefs.Save();

        SceneManager.LoadScene("TelaEndGame");
    }

    public void restartGame()
    {
        restarter.Restart();
    }
}
