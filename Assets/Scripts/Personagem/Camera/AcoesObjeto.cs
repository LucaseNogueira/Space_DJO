using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcoesObjeto : MonoBehaviour
{
    public GameObject GameController;
    public AudioClip efeitoPontuar;

    private IdentificarObjeto idObjeto;

    // Start is called before the first frame update
    void Start()
    {
        idObjeto = GetComponent<IdentificarObjeto>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(idObjeto.GetObjVisualizar() != null)
            {
                Visualizar();
            }

            if(idObjeto.GetObjPontuacao() != null)
            {
                Pontuar();
            }
        }
    }

    private void Visualizar()
    {
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;

        if(Time.timeScale == 0f)
        {
            GameController.GetComponent<GameController>().ShowMonitor(idObjeto.GetObjVisualizar());
        }else
        {
            GameController.GetComponent<GameController>().CloseMonitor();
        }
    }

    private void Pontuar()
    {
        EfeitoSonoro();
        Destroy(idObjeto.GetObjPontuacao());
        GameController.GetComponent<GameController>().AdicionarPontuacao();
        idObjeto.LimparTexto();
    }

    private void EfeitoSonoro()
    {
        if(gameObject.GetComponent<AudioSource>() != null && efeitoPontuar != null)
        {
            gameObject.GetComponent<AudioSource>().clip = efeitoPontuar;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
    
}
