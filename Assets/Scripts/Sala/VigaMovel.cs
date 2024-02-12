using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigaMovel : MonoBehaviour
{
    /*Eixos X, Y, Z*/
    public char eixoMovel;
    public float velocidade = 0.1f;
    /*Exemplo: caso true move para frente, caso false o contrario*/
    public bool sentidoEixoMovel = true;

    // Start is called before the first frame update
    void Start()
    {
        if(sentidoEixoMovel == false && velocidade < 0)
        {
            velocidade = velocidade * -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Mover();
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.tag == "Player")
        {
            StartCoroutine(Congelar(col));
        }
        else
        {
            if(col.transform.gameObject.tag != "CuboSpawn")
            {
                sentidoEixoMovel = !sentidoEixoMovel;
                velocidade = velocidade * -1;
            }
        }
    }
    
    IEnumerator Congelar(Collider col)
    {
        MovimentarPersonagem mp = col.transform.gameObject.GetComponent<MovimentarPersonagem>();

        if(mp.GetCongelarPersonagem() == false)
        {
            EfeitoSonoro();
            mp.CongelarPersonagem();
            yield return new WaitForSeconds(3f);
            mp.DescongelarPersonagem();
        }
    }

    private void Mover()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        switch(eixoMovel)
        {
            case 'x':
            case 'X':
                pos = new Vector3(velocidade, 0, 0);
            break;
            case 'y':
            case 'Y':
                pos = new Vector3(0, velocidade, 0);
            break;
            case 'z':
            case 'Z':
                pos = new Vector3(0, 0, velocidade);
            break;
            default:
                print("Precisa informar o eixo que o objeto sera movido");
            break;
        }

        transform.position += pos;
    }

    public void EfeitoSonoro()
    {
        if(gameObject.GetComponent<AudioSource>() != null && gameObject.GetComponent<AudioSource>().clip != null)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
