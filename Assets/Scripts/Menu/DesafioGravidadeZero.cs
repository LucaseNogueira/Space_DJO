using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesafioGravidadeZero : Desafio
{
    protected override void InitTitulo()
    {
        titulo = "Gravidade Zero";
    }

    protected override void InitMensagem()
    {
        mensagem =  "Neste simulador Ubiratan deve adaptar-se ao ambiente de gravidade zero. " + 
                    "Ubiratan irá navegar por um ambiente simulado de gravidade zero, " + 
                    "coletando objetos flutuantes em uma ordem específica. " + 
                    "Obstáculos móveis e correntes de ar podem dificultar a coleta.";
    }
    
    protected override void InitPontoDesafio()
    {
        pontoDesafio = "4";
    }

    protected override void InitPontoUbiratan()
    {
        pontoUbiratan = "Indefinido";
        if(PlayerPrefs.HasKey("DesafioGravidadeScore") == true)
        {
            pontoUbiratan = "" + PlayerPrefs.GetInt("DesafioGravidadeScore");
        }
    }
}
