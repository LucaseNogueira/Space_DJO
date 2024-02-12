using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjetos : MonoBehaviour
{
   public GameObject[] SpawnItens;
   int random;
   public float spawntime;
   public float spawndelay;

    void Start () 
    {
        InvokeRepeating("SpawnRandom", spawntime, spawndelay);
    }
    
    void SpawnRandom(){ 
        random= Random.Range(0, SpawnItens.Length);
        Instantiate(SpawnItens[random],transform.position, transform.rotation);
    }
}
