using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePoint : MonoBehaviour
{
    public AudioClip clip;
    public GameObject gameController;
    public int idScene = 999;
    public bool limparFalasAnteriores = false;

    private bool ativado = false;
    
    public void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.tag == "Player")
        {
            AtualizarFala();
            AlterarScene();
        }
    }

    private void AtualizarFala()
    {
        if(clip != null &&
           ativado == false)
        {
            IGameController game;
            if(gameController.GetComponent<GameControllerVoo>())
            {
                game = gameController.GetComponent<GameControllerVoo>();
            }else
            {
                game = gameController.GetComponent<GameController>();
            }

            if(limparFalasAnteriores == true)
            {
                game.LimparFalas();
                game.LimparAudioSource();
            }
            game.InterromperFala(clip);
            ativado = true;
        }
    }

    private void AlterarScene()
    {
        IGameController game = null;
        if(gameController.transform.Find("GameControllerVoo"))
        {
            game = gameController.GetComponent<GameControllerVoo>();
        }else
        {
            game = gameController.GetComponent<GameController>();
        }
        switch(idScene)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                game.AlterarScene(idScene);
                break;
            default:
                idScene = 999;
                break;
        }
    }
}
