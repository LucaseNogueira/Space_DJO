using System.Collections.Generic;

[System.Serializable]
public class Checkpoint
{
    public string save;
    public int scorePousoEmergencia;
    public int scoreGravidade;
    public int scoreAsteroides;

    public override string ToString()
    {
        string resultado = "Save: " + save + "\n";
        resultado += "ScorePousoEmergencia: " + scorePousoEmergencia + "\n";
        resultado += "scoreGravidade: " + scoreGravidade + "\n";
        resultado += "scoreAsteroides: " + scoreAsteroides + "\n";
        
        return resultado;
    }
}
