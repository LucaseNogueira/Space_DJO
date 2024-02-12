using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private TextMeshProUGUI contentText;
    public GameObject rankingList;

    public CreateDinamicButtom createDinamicButtom;

    private void Start()
    {
        GameObject GOContentText = GameObject.Find("ContentText");
        contentText = GOContentText.GetComponent<TextMeshProUGUI>();
        //rankingList = GameObject.Find("RankingList");
    }

    public void NewGame()
    {
        contentText.gameObject.SetActive(false);
        rankingList.SetActive(false);

        createDinamicButtom.DestruirBotoes();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Academia");
    }

    public void Reload()
    {
        contentText.gameObject.SetActive(false);
        rankingList.SetActive(false);

        List<Checkpoint> listaCheckpoint = CheckpointController.GetInstance().GetAllCheckpoint();
        for(int i = listaCheckpoint.Count - 1; i >= 0; i--)
        {
            Checkpoint checkpoint = listaCheckpoint[i];
            createDinamicButtom.CriarBotao(checkpoint.save, () => {
                int id = listaCheckpoint.IndexOf(checkpoint);
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("LoadCheckpoint", id);
                SceneManager.LoadScene("Academia");
            });
        }
    }

    public void Ranking()
    {
        createDinamicButtom.DestruirBotoes();

        List<DadosJogador> jogadores = RankingController.Instance.ReadRanking();
        
        int i = 1;
        foreach (DadosJogador player in jogadores)
        {
            Transform itemListObj = rankingList.transform.Find("Item_" + i);
            itemListObj.gameObject.SetActive(true);

            int totalPontos = 0;
            string pontuacao = "";
            foreach(PontuacaoDesafio ponto in player.pontuacoes)
            {
                totalPontos += ponto.pontuacao;
                pontuacao += ponto.pontuacao + "\n";
            }

            itemListObj.Find("Titulo").GetComponent<Text>().text = player.nome;
            itemListObj.Find("Total").GetComponent<Text>().text = totalPontos + "";
            itemListObj.Find("Pontuacao").GetComponent<Text>().text = pontuacao;
            i++;
        }

        contentText.gameObject.SetActive(false);
        rankingList.SetActive(true);
    }

    public void About()
    {
        createDinamicButtom.DestruirBotoes();

        contentText.text = "Alexander Felipe Chiudini Ristow (<link=\"email1\"><color=#0000FF>alexander.chiudini.eng@gmail.com</color></link>)\nLucas Eduardo Nogueira (<link=\"email2\"><color=#0000FF>lucasenogueira@outlook.com</color></link>)";

        contentText.gameObject.SetActive(true);
        rankingList.SetActive(false);
    }
}
