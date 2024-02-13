using System.Collections.Generic;

[System.Serializable]
public class DadosJogador
{
    public string nome;
    public List<PontuacaoDesafio> pontuacoes = new List<PontuacaoDesafio>();

    public override string ToString()
    {
        string resultado = "Nome: " + nome + "\nPontuações:\n";
        foreach (PontuacaoDesafio pd in pontuacoes)
        {
            resultado += pd.ToString() + "\n";
        }
        return resultado;
    }
}
