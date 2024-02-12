using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiGates : MonoBehaviour
{
    private Animator anim;
    private bool estaAberta;
    private bool estaColidindo;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        estaAberta = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Acionar()
    {
        EfeitoSonoro();

        if(estaAberta == false)
        {
            anim.Play("AbrirSciFiGate");
            StartCoroutine(esperar());
        }else
        {
            anim.Play("FecharSciFiGate");
            StartCoroutine(esperar());
        }
    }

    IEnumerator esperar()
    {
        yield return new WaitForSeconds(0.1f);
        transform.Find("Door_Left").gameObject.SetActive(estaAberta);
        transform.Find("Door_Right").gameObject.SetActive(estaAberta);
        estaAberta = !estaAberta;
    }

    public void EfeitoSonoro()
    {
        if(gameObject.GetComponent<AudioSource>() != null && gameObject.GetComponent<AudioSource>().clip != null)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
    
}
