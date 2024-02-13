using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    public Text textDesafio;

    public Text textScore;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SetChallengeInformation();
        SetScoreInformation();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("RestartSceneToLoad"));
    }

    public void ExitGame()
    {
        PlayerPrefs.SetInt("MiniGameFinished", 1);
        SceneManager.LoadScene(PlayerPrefs.GetString("ExitSceneToLoad"));
    }

    private void SetChallengeInformation()
    {
        if (textDesafio == null)
        {
            textDesafio = GameObject.Find("GameType").GetComponent<Text>();
        }

        if (!textDesafio.gameObject.activeInHierarchy)
        {
            textDesafio.gameObject.SetActive(true);
        }

        textDesafio.text = "Desafio: " + PlayerPrefs.GetString("MiniGameLastPlayed");
    }

    private void SetScoreInformation()
    {
        if (textScore == null)
        {
            textScore = GameObject.Find("ScoreAchieved").GetComponent<Text>();
        }

        if (!textScore.gameObject.activeInHierarchy)
        {
            textScore.gameObject.SetActive(true);
        }

        textScore.text = "Pontuação Alcançada: " + PlayerPrefs.GetInt("MiniGameScore");


        if(PlayerPrefs.GetString("RestartSceneToLoad") == "DesafioGravidade")
        {
            PlayerPrefs.SetInt("DesafioGravidadeScore", PlayerPrefs.GetInt("MiniGameScore"));
        }
        if(PlayerPrefs.GetString("RestartSceneToLoad") == "VooComObstaculos")
        {
            PlayerPrefs.SetInt("DesafioPousoEmergenciaScore", PlayerPrefs.GetInt("MiniGameScore"));
        }
        if(PlayerPrefs.GetString("RestartSceneToLoad") == "Asteroids")
        {
            PlayerPrefs.SetInt("DesafioDestruirAsteroideScore", PlayerPrefs.GetInt("MiniGameScore"));
        }
        
        PlayerPrefs.SetInt("RankingAndCheckpoint", 1);
        //Criar novo checkpoint (CheckpointController)
        CheckpointController.GetInstance().AddCheckPoint();
        //Pegar o ultimo checkpoint criado e adicionar na prefs LoadCheckpoint
        int checkpointPosition = CheckpointController.GetInstance().GetLastCheckpointId();
        PlayerPrefs.SetInt("LoadCheckpoint", checkpointPosition);
    }
}
