using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameControllerVoo : MonoBehaviour, IGameController
{
    public Text scoreText;

    public static GameControllerVoo Instance { get; private set; }

    private int points = 0;
    public GameObject areaPause;

    private AudioSource audioSource;
    private Stack<AudioClip> audiosTocar;
    private Stack<float> tempoTocar;
    public List<AudioClip> audiosAllan;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // Se a cena atual é a mesma que a cena do Singleton existente, destrua este objeto.
            if (SceneManager.GetActiveScene().name == Instance.gameObject.scene.name)
            {
                Destroy(gameObject);
            }
            else
            {
                // Se a cena é diferente, isso significa que estamos carregando uma nova cena
                // onde este Singleton ainda não existe. Portanto, atualize a instância.
                Destroy(Instance.gameObject);
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    void Start()
    {
        UpdateScoreDisplay();
        audioSource = GetComponent<AudioSource>();
        InitAudiosTocar();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Pausar();
        }
        TocarFala();
    }

    public void AdicionarFala(AudioClip clip)
    {
        audiosTocar.Push(clip);
    }

    public void InterromperFala(AudioClip novoAudio)
    {
        if(audiosTocar.Count > 0 || audioSource.isPlaying){
            //Adicionando o audio interrompido na var audiosTocar para que ele possa ser tocado novamente.
            audiosTocar.Push(audioSource.clip);
            //Adicionando o time em tempo tocado para que o audio interrompido possa ser tocado de onde parou.
            tempoTocar.Push(audioSource.time);

            audioSource.Stop();
        }

        //Adicionando o novo audio que será tocado
        audiosTocar.Push(novoAudio);
        tempoTocar.Push(0f);
    }

    public void LimparFalas()
    {
        audiosTocar.Clear();
        tempoTocar.Clear();
    }

    public void LimparAudioSource()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    public void AddScore(int score)
    {
        points += score;
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("VooScore", points);

        PlayerPrefs.SetInt("MiniGameScore", points);
        PlayerPrefs.SetString("ExitSceneToLoad", "Academia");
        PlayerPrefs.SetString("RestartSceneToLoad", "VooComObstaculos");
        PlayerPrefs.SetString("MiniGameLastPlayed", "Voo com Obstaculos");

        PlayerPrefs.Save();

        SceneManager.LoadScene("TelaEndGame");
    }

    public void RestartGame()
    {
        StartCoroutine(RestartScene());
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AlterarScene(int i){}

    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + points.ToString();
    }

    private void InitAudiosTocar()
    {
        audiosTocar = new Stack<AudioClip>();
        tempoTocar  = new Stack<float>();
        foreach(var audio in audiosAllan)
        {
            audiosTocar.Push(audio);
            tempoTocar.Push(0f);
        }
    }

    private void TocarFala()
    {
        if(audioSource.isPlaying == false && audiosTocar.Count > 0){
            audioSource.clip = audiosTocar.Pop();
            audioSource.time = tempoTocar.Pop();
            audioSource.Play();
        }
    }

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

    public void ShowTelaPause()
    {
        areaPause.SetActive(true);
        HabilitarCursor();
    }

    public void CloseTelaPause()
    {
        areaPause.SetActive(false);
        DesabilitarCursor();
    }

    public void HabilitarCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DesabilitarCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
