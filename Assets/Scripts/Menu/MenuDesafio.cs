using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDesafio : MonoBehaviour
{
    public GameObject gameController;

    private string idNome;
    private bool estavaAtivo;
    private GameObject monitor;

    // Start is called before the first frame update
    void Start()
    {
        idNome = "";
        estavaAtivo = gameObject.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitMenuDesafio()
    {
        IDesafio desafio = new DesafioIndefinido();
        switch(idNome)
        {
            case "Monitor_Gravidade":
                desafio = new DesafioGravidadeZero();
            break;
            case "Monitor_Emergencia":
                desafio = new DesafioPousoEmergencia();
            break;
            case "Monitor_Asteroides":
                desafio = new DesafioDestruirAsteroides();
            break;
            default:
                Debug.LogWarning("Não existe um desafio para este monitor");
            break;
        }

        if(desafio != null)
        {
            transform.Find("titulo").GetComponent<TextMeshProUGUI>().SetText(desafio.GetTitulo());
            transform.Find("mensagem").GetComponent<TextMeshProUGUI>().SetText(desafio.GetMensagem());
            transform.Find("AreaPontoDesafio").Find("pontuacao").GetComponent<TextMeshProUGUI>().SetText(desafio.GetPontoDesafio());
            transform.Find("AreaPontoUbiratan").Find("pontuacao").GetComponent<TextMeshProUGUI>().SetText(desafio.GetPontoUbiratan());
        }
    }

    public void Entrar()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        gameController.GetComponent<GameController>().CloseMonitor();
        GameObject porta = monitor.transform.parent.Find("SciFiGates").gameObject;
        porta.GetComponent<SciFiGates>().Acionar();

        if(monitor.GetComponent<Monitor>().GetAudioclip() != null)
        {
            gameController.GetComponent<GameController>().LimparFalas();
            gameController.GetComponent<GameController>().LimparAudioSource();
            AudioClip audio = monitor.GetComponent<Monitor>().GetAudioclip();
            gameController.GetComponent<GameController>().InterromperFala(audio);
        }
    }

    public void Sair()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        gameController.GetComponent<GameController>().CloseMonitor();
    }

    public void SetIdNome(string nome)
    {
        idNome = nome;
    }

    public bool GetEstavaAtivo()
    {
        return estavaAtivo;
    }

    public void SetEstavaAtivo(bool b)
    {
        estavaAtivo = b;
    }

    public GameObject GetMonitor()
    {
        return monitor;
    }

    public void SetMonitor(GameObject porta)
    {
        monitor = porta;
    }
}
