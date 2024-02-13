using System.Collections;
using System.Collections.Generic;

public interface IDesafio
{
    string GetTitulo();
    string GetMensagem();
    string GetPontoDesafio();
    string GetPontoUbiratan();

    void SetPontoUbiratan(string ponto);
}
