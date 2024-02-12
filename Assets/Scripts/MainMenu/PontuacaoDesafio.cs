[System.Serializable]
public class PontuacaoDesafio
{
    public string desafio;
    public int pontuacao;

    public override string ToString()
    {
        return desafio + ": " + pontuacao + " pontos";
    }
}