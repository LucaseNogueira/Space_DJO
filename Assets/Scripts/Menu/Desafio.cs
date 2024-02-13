using System.Collections;
using System.Collections.Generic;

public abstract class Desafio : IDesafio
{
    protected string titulo;
    protected string mensagem;
    protected string pontoDesafio;
    protected string pontoUbiratan;

    protected abstract void InitTitulo();
    protected abstract void InitMensagem();
    protected abstract void InitPontoDesafio();
    protected abstract void InitPontoUbiratan();

    public Desafio(){
        InitTitulo();
        InitMensagem();
        InitPontoDesafio();
        InitPontoUbiratan();
    }

    public string GetTitulo()
    {
        return titulo;
    }

    public string GetMensagem()
    {
        return mensagem;
    }

    public string GetPontoDesafio()
    {
        return pontoDesafio;
    }

    public string GetPontoUbiratan()
    {
        return pontoUbiratan;
    }

    public void SetPontoUbiratan(string ponto)
    {
        pontoUbiratan = ponto;
    }
}
