using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RankingController : MonoBehaviour
{
    public static RankingController Instance { get; private set; }

    private List<DadosJogador> listaJogadores = new List<DadosJogador>();

    private string caminhoDoArquivo;

    private void Awake()
    {
        string caminhoRaizProjeto = Application.dataPath + "/data/";
        caminhoDoArquivo = System.IO.Path.Combine(caminhoRaizProjeto, "Ranking.json");
        GenerateDefaultRanking();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GenerateDefaultRanking()
    {
        listaJogadores = ReadRanking();

        if(listaJogadores.Count == 0)
        {
            AddInRanking(new DadosJogador
            {
                nome = "Professor Adilson Save --/--/-- --:--:--",
                pontuacoes = new List<PontuacaoDesafio>
                {
                    new PontuacaoDesafio { desafio = "Voo Com Obstaculos", pontuacao = 500 },
                    new PontuacaoDesafio { desafio = "Asteroids", pontuacao = 1500 },
                    new PontuacaoDesafio { desafio = "Simulador de Gravidade", pontuacao = 5 }
                }
            });

            AddInRanking(new DadosJogador
            {
                nome = "Professor Geraldo Save --/--/-- --:--:--",
                pontuacoes = new List<PontuacaoDesafio>
                {
                    new PontuacaoDesafio { desafio = "Voo Com Obstaculos", pontuacao = 490 },
                    new PontuacaoDesafio { desafio = "Asteroids", pontuacao = 1300 },
                    new PontuacaoDesafio { desafio = "Simulador de Gravidade", pontuacao = 5 }
                }
            });
        }
    }

    public void AddInRanking(DadosJogador novoJogador)
    {
        int somaPontuacaoNovoJogador = novoJogador.pontuacoes.Sum(p => p.pontuacao);

        listaJogadores = ReadRanking();

        if (listaJogadores.Count < 5 || somaPontuacaoNovoJogador > listaJogadores.Min(j => j.pontuacoes.Sum(p => p.pontuacao)))
        {
            listaJogadores.Add(novoJogador);
        }

        listaJogadores = listaJogadores
            .OrderByDescending(j => j.pontuacoes.Sum(p => p.pontuacao))
            .Take(5)
            .ToList();

        SaveRanking();
    }

    private void SaveRanking()
    {
        ListaJogadores listaJogadoresWrapper = new ListaJogadores { jogadores = listaJogadores };
        string json = JsonUtility.ToJson(listaJogadoresWrapper);
        
        System.IO.File.WriteAllText(caminhoDoArquivo, json);
    }

    public List<DadosJogador> ReadRanking()
    {
        List<DadosJogador> jogadores = new List<DadosJogador>();

        if(System.IO.File.Exists(caminhoDoArquivo))
        {
            string jsonLido = System.IO.File.ReadAllText(caminhoDoArquivo);
            ListaJogadores listaJogadoresLida = JsonUtility.FromJson<ListaJogadores>(jsonLido);

            if (!listaJogadoresLida.IsUnityNull())
            {
                jogadores = listaJogadoresLida.jogadores;
            }
        }

        return jogadores;
    }
}
