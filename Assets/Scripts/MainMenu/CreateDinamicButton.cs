using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateDinamicButtom : MonoBehaviour
{
    public GameObject botaoPrefab;
    public Transform parentParaBotao;

    private int contadorBotao = 0;
    private float alturaBotao = 50f;
    private float espacamento = 10f;
    private float posicaoInicialY = 100f;

    private List<GameObject> botoesCriados = new List<GameObject>();

    public void CriarBotao(string textoBotao, UnityEngine.Events.UnityAction acao)
    {
        GameObject botaoObj = Instantiate(botaoPrefab, parentParaBotao);
        botaoObj.GetComponentInChildren<TextMeshProUGUI>().text = textoBotao;
        botaoObj.GetComponent<Button>().onClick.AddListener(acao);

        RectTransform rectTransform = botaoObj.GetComponent<RectTransform>();
        float yPosition = posicaoInicialY - contadorBotao * (alturaBotao + espacamento);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, yPosition);
        rectTransform.sizeDelta = new Vector2(400f, alturaBotao);

        contadorBotao++;

        botoesCriados.Add(botaoObj);
    }

    public void DestruirBotoes()
    {
        foreach (var botao in botoesCriados)
        {
            Destroy(botao);
        }

        botoesCriados.Clear();
        contadorBotao = 0;
    }
}
