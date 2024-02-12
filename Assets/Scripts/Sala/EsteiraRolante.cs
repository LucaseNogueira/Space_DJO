using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsteiraRolante : MonoBehaviour
{
    private ArrayList objetos = new ArrayList();
    public float velocidade_Z = 0.1f;
    public float velocidade_X = 0f;

    void Awake()
    {

    }

    void FixedUpdate()
    {
        Movimento();
    }

    private void Movimento()
    {
        foreach(Transform objeto in objetos)
        {
            objeto.position += new Vector3(velocidade_X, 0, velocidade_Z);
        }
        
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.tag == "Player")
        {
            EfeitoSonoro();
        }

        objetos.Add(col.transform);
    }

    public void OnTriggerExit(Collider col)
    {
        objetos.Remove(col.transform);
        if(col.transform.gameObject.tag == "Player")
        {
            PausarEfeitoSonoro();
        }else
        {
            Destroy(col.transform.gameObject);
        }
    }

    private void EfeitoSonoro()
    {
        if(gameObject.GetComponent<AudioSource>() != null           &&
           gameObject.GetComponent<AudioSource>().clip != null      &&
           gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void PausarEfeitoSonoro()
    {
        if(gameObject.GetComponent<AudioSource>() != null           &&
           gameObject.GetComponent<AudioSource>().clip != null      &&
           gameObject.GetComponent<AudioSource>().isPlaying == true)
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}
