using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CheckpointController
{
    private string fileName = "Checkpoint";
    private string fileType = ".json";
    private string filePath = "data";
    private int idLastCheckpoint;
    private string lastDateTime;

    private static CheckpointController instance;
    private CheckpointController(){}

    public static CheckpointController GetInstance()
    {
        if(instance == null)
        {
            instance = new CheckpointController();
        }

        return instance;
    }

    public void AddCheckPoint()
    {
        Checkpoint newCheckpoint = CreateCheckpoint();
        List<Checkpoint> listaCheckpoint = new List<Checkpoint>();
        //Verifica se o arquivo existe:
        //Caso existir o arquivo, devemos capturar as informacoes deste arquivo e valida-la;
        if(ExisteArquivo())
        {
            string json = File.ReadAllText(GetCaminhoArquivo());
            ListaCheckpoint readList = JsonUtility.FromJson<ListaCheckpoint>(json);

            if (readList != null)
            {
                listaCheckpoint = readList.checkpoints;
                if(listaCheckpoint.Count == 5)
                {
                    listaCheckpoint.RemoveAt(0);
                }
            }
        }

        listaCheckpoint.Add(newCheckpoint);
        idLastCheckpoint = listaCheckpoint.IndexOf(newCheckpoint);
        ListaCheckpoint listaWrapper = new ListaCheckpoint { checkpoints = listaCheckpoint };
        string dataJSON = JsonUtility.ToJson(listaWrapper);
        File.WriteAllText(GetCaminhoArquivo(), dataJSON);
    }

    public void LoadCheckpoint(int idCheckpoint)
    {
        if(ExisteArquivo())
        {
            string json = File.ReadAllText(GetCaminhoArquivo());
            ListaCheckpoint readList = JsonUtility.FromJson<ListaCheckpoint>(json);
            Checkpoint checkpoint = readList.checkpoints[idCheckpoint];

            PlayerPrefs.SetInt("DesafioGravidadeScore", checkpoint.scoreGravidade);
            PlayerPrefs.SetInt("DesafioPousoEmergenciaScore", checkpoint.scorePousoEmergencia);
            PlayerPrefs.SetInt("DesafioDestruirAsteroideScore", checkpoint.scoreAsteroides);
        }
    }

    public List<Checkpoint> GetAllCheckpoint()
    {
        List<Checkpoint> listaCheckpoint = new List<Checkpoint>();
        if(ExisteArquivo())
        {
            string json = File.ReadAllText(GetCaminhoArquivo());
            ListaCheckpoint readList = JsonUtility.FromJson<ListaCheckpoint>(json);

            listaCheckpoint = readList.checkpoints;
        }

        return listaCheckpoint;
    }

    public bool HasAllScore()
    {
        return PlayerPrefs.HasKey("DesafioGravidadeScore") &&
               PlayerPrefs.HasKey("DesafioPousoEmergenciaScore") &&
               PlayerPrefs.HasKey("DesafioDestruirAsteroideScore") &&
               PlayerPrefs.GetInt("DesafioGravidadeScore") != 0 &&
               PlayerPrefs.GetInt("DesafioPousoEmergenciaScore") != 0 &&
               PlayerPrefs.GetInt("DesafioDestruirAsteroideScore") != 0;
    }

    public string GetCaminhoArquivo()
    {
        return Application.dataPath + "/" + filePath + "/" + fileName + fileType;
    }

    public bool ExisteArquivo()
    {
        return File.Exists(GetCaminhoArquivo());
    }

    public int GetLastCheckpointId()
    {
        return idLastCheckpoint;
    }

    public string GetLastDateTime()
    {
        return lastDateTime;
    }

    private Checkpoint CreateCheckpoint()
    {
        DateTime dataHoraAtual = DateTime.Now;
        lastDateTime = dataHoraAtual.ToString("dd/MM/yyyy HH:mm:ss");
        Checkpoint data = new Checkpoint();
        data.save = "Save " + dataHoraAtual.ToString("dd/MM/yyyy HH:mm:ss");
        data.scorePousoEmergencia = PlayerPrefs.GetInt("DesafioPousoEmergenciaScore");
        data.scoreGravidade = PlayerPrefs.GetInt("DesafioGravidadeScore");
        data.scoreAsteroides = PlayerPrefs.GetInt("DesafioDestruirAsteroideScore");
        
        return data;
    }
}