using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FecharPorta : MonoBehaviour
{

    public GameObject gameController, sciFiGate;

    private bool acionado = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
        if(acionado == false){
            sciFiGate.GetComponent<SciFiGates>().Acionar();
            acionado = true;
        }
    }
}
