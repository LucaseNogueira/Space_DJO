using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IGameController
{
    public GameObject menuDesafio, areaPause, areaPontuacao, areaCheckpoint;
    public List<AudioClip> audiosAllan;
    public List<AudioClip> audiosAllanAlterantivo;
    public int idScene;

    private AudioSource audioSource;
    private Stack<AudioClip> audiosTocar;
    private Stack<float> tempoTocar;

    private int pontuacaoDesafio;
    
    // Start is called before the first frame update
    void Start()
    {
        InitCheckpoint();
        DesabilitarCursor();
        audioSource = GetComponent<AudioSource>();
        InitPlayerPrefs();
        InitAudiosTocar();
        InitPontuacaoDesafio();

        PlayerPrefs.DeleteKey("LoadCheckpoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Pausar();
        }
        TocarFala();
        AtualizarScene();
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

    public void AlterarScene(int id)
    {
        idScene = id;
    }

    private void AtualizarScene()
    {
        if(audioSource.isPlaying == false && idScene != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(idScene);
        }
    }

    private void InitCheckpoint()
    {
        if(idScene == 1){
            if(PlayerPrefs.HasKey("LoadCheckpoint"))
            {
                int idLoad = PlayerPrefs.GetInt("LoadCheckpoint");
                
                CheckpointController.GetInstance().LoadCheckpoint(idLoad);

                if(PlayerPrefs.HasKey("RankingAndCheckpoint"))
                {
                    AddPlayerInRanking();
                    StartCoroutine(CanvasCheckpoint());
                    PlayerPrefs.DeleteKey("RankingAndCheckpoint");
                }
            }
        }
    }

    IEnumerator CanvasCheckpoint()
    {
        areaCheckpoint.SetActive(true);
        yield return new WaitForSeconds(5f);
        areaCheckpoint.SetActive(false);
    }

    private void InitPlayerPrefs()
    {
        if(idScene == 1)
        {
            PlayerPrefs.SetString("RestartSceneToLoad", "");
            PlayerPrefs.SetString("ExitSceneToLoad", "");
        }
        if(idScene == 2)
        {
            PlayerPrefs.SetString("MiniGameLastPlayed", "Simulador de Gravidade");
            PlayerPrefs.SetInt("MiniGameScore", 0);
            PlayerPrefs.SetString("RestartSceneToLoad", "DesafioGravidade");
            PlayerPrefs.SetString("ExitSceneToLoad", "Academia");
        }
    }

    private void InitAudiosTocar()
    {
        audiosTocar = new Stack<AudioClip>();
        tempoTocar  = new Stack<float>();
        
        if(idScene == 1 && PlayerPrefs.HasKey("LoadCheckpoint") == true)
        {
            AudioClip audio = null;
            if(CheckpointController.GetInstance().HasAllScore())
            {
                audio = audiosAllanAlterantivo[1];
            }else
            {
                audio = audiosAllanAlterantivo[0];
            }

            audiosTocar.Push(audio);
            tempoTocar.Push(0f);
        }else
        {
            foreach(var audio in audiosAllan)
            {
                audiosTocar.Push(audio);
                tempoTocar.Push(0f);
            }
        }
    }

    private void InitPontuacaoDesafio()
    {
        if(areaPontuacao != null)
        {
            pontuacaoDesafio = 0;
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

    public void ShowMonitor(GameObject monitor)
    {
        if(areaPause.activeSelf == false)
        {
            menuDesafio.SetActive(true);
            menuDesafio.GetComponent<MenuDesafio>().SetMonitor(monitor);
            menuDesafio.GetComponent<MenuDesafio>().SetIdNome(monitor.name);
            menuDesafio.GetComponent<MenuDesafio>().InitMenuDesafio();
            HabilitarCursor();
        }
    }

    public void CloseMonitor()
    {
        if(areaPause.activeSelf == false){
            menuDesafio.SetActive(false);
            menuDesafio.GetComponent<MenuDesafio>().SetIdNome("");
            DesabilitarCursor();
        }
    }

    public void ShowTelaPause()
    {
        if(menuDesafio.activeSelf){
            menuDesafio.SetActive(false);
            menuDesafio.GetComponent<MenuDesafio>().SetEstavaAtivo(true);
        }
        
        areaPause.SetActive(true);
        HabilitarCursor();
    }

    public void CloseTelaPause()
    {
        if(menuDesafio.GetComponent<MenuDesafio>().GetEstavaAtivo()){
            menuDesafio.SetActive(true);
            menuDesafio.GetComponent<MenuDesafio>().SetEstavaAtivo(false);
        }
        
        areaPause.SetActive(false);
        DesabilitarCursor();
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

    public void AdicionarPontuacao(){
        if(areaPontuacao != null)
        {
            pontuacaoDesafio++;
            PlayerPrefs.SetInt("MiniGameScore", pontuacaoDesafio);
            string texto = pontuacaoDesafio + " / 4";
            areaPontuacao.transform.Find("pontuacao").GetComponent<TextMeshProUGUI>().SetText(texto);
        }
    }

    private void TocarFala()
    {
        if(audioSource.isPlaying == false){
            if(audiosTocar.Count > 0){
            audioSource.clip = audiosTocar.Pop();
            audioSource.time = tempoTocar.Pop();
            audioSource.Play();
            }
        }
    }

    private void AddPlayerInRanking()
    {
        string dataHora = CheckpointController.GetInstance().GetLastDateTime();
        DadosJogador player = new DadosJogador();
        player.nome = "Ubiratan Save " + dataHora;
        player.pontuacoes = new List<PontuacaoDesafio>{
            new PontuacaoDesafio { desafio = "Voo Com Obstaculos", pontuacao = PlayerPrefs.GetInt("DesafioPousoEmergenciaScore") },
            new PontuacaoDesafio { desafio = "Asteroids", pontuacao = PlayerPrefs.GetInt("DesafioDestruirAsteroideScore") },
            new PontuacaoDesafio { desafio = "Simulador de Gravidade", pontuacao = PlayerPrefs.GetInt("DesafioGravidadeScore") }
        };

        RankingController.Instance.AddInRanking(player);
    }
}
