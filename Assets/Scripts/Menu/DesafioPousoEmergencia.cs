using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesafioPousoEmergencia : Desafio
{
    protected override void InitTitulo()
    {
        titulo = "Pouso de Emergência";
    }

    protected override void InitMensagem()
    {
        mensagem =  "Ajude o Ubiratan num simulador de pouso lunar. " + 
                    "Utilize os controles para manobrar a nave, " + 
                    "evitando obstáculos como rochas e crateras para assim pousar suavemente em uma área designada.";
    }
    
    protected override void InitPontoDesafio()
    {
        pontoDesafio = "320";
    }

    protected override void InitPontoUbiratan()
    {
        pontoUbiratan = "Indefinido";
        if(PlayerPrefs.HasKey("DesafioPousoEmergenciaScore") == true)
        {
            pontoUbiratan = "" + PlayerPrefs.GetInt("DesafioPousoEmergenciaScore");
        }
    }
}
