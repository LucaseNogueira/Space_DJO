using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesafioDestruirAsteroides : Desafio
{
    protected override void InitTitulo()
    {
        titulo = "Destruir Asteroides";
    }

    protected override void InitMensagem()
    {
        mensagem =  "Este simulador treina as habilidades de combate e defesa de Ubiratan com as armas da nave. " + 
                    "O objetivo é destruir todos os asteroides antes que eles colidam com a nave.";
    }
    
    protected override void InitPontoDesafio()
    {
        pontoDesafio = "Indefinido";
    }

    protected override void InitPontoUbiratan()
    {
        pontoUbiratan = "Indefinido";
        if(PlayerPrefs.HasKey("DesafioDestruirAsteroideScore") == true)
        {
            pontoUbiratan = "" + PlayerPrefs.GetInt("DesafioDestruirAsteroideScore");
        }
    }
}
