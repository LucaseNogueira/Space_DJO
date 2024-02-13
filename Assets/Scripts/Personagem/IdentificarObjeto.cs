using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentificarObjeto : MonoBehaviour
{
    public Text textoTecla, textoMsg;

    private float distanciaAlvo;
    private GameObject objAlvo;
    private GameObject objVisualizar, objPontuacao;

    // Start is called before the first frame update
    void Start()
    {
        objAlvo = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % 5 == 0)
        {
            objVisualizar = null;
            objPontuacao = null;

            int ignorarLayer = 7; //ignoreplayercast
            ignorarLayer = 1 << ignorarLayer;
            ignorarLayer = ~ignorarLayer; //somente o layer 7 sera ignorado

            RaycastHit hit;
            if(Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward), out hit, 5, ignorarLayer))
            {
                distanciaAlvo = hit.distance;
                if(objAlvo != null && hit.transform.gameObject != objAlvo)
                {
                    objAlvo.GetComponent<Outline>().OutlineWidth = 0f;
                    objAlvo = null;
                    LimparTexto();
                }

                if(hit.transform.gameObject.tag == "Visualizar")
                {
                    objVisualizar = hit.transform.gameObject;
                    objAlvo = objVisualizar;
                    textoTecla.color = new Color(248/255f,248/255f,13/255f);
                    textoMsg.color = textoTecla.color;
                    textoTecla.text = "[F]";
                    textoMsg.text = "Visualizar";
                }

                if(hit.transform.gameObject.tag == "Pontuar")
                {
                    objPontuacao = hit.transform.gameObject;
                    objAlvo = objPontuacao;

                    textoTecla.color = new Color(51/255f, 1, 0);
                    textoMsg.color = textoTecla.color;
                    textoTecla.text = "[F]";
                    textoMsg.text = "Pegar";
                }

                if(objAlvo != null)
                {
                    objAlvo.GetComponent<Outline>().OutlineWidth = 5f;
                }
            }else
            {
                if(objAlvo != null)
                {
                    objAlvo.GetComponent<Outline>().OutlineWidth = 0f;
                    objAlvo = null;
                    LimparTexto();
                }
            }
        }
    }

    public GameObject GetObjVisualizar()
    {
        return objVisualizar;
    }

    public GameObject GetObjPontuacao()
    {
        return objPontuacao;
    }

    public void LimparTexto()
    {
        textoTecla.text = "";
        textoMsg.text = "";
    }
}
