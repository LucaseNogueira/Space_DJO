using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaPause : MonoBehaviour
{
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Voltar()
    {
        gameController.GetComponent<GameController>().CloseTelaPause();
    }

    public void Sair()
    {
        SceneManager.LoadScene(0);
    }

    public void HabilitarAreaPrincipal()
    {
        GameObject area = transform.Find("AreaPrincipal").gameObject;
        if(area.activeSelf == false)
        {
            area.SetActive(true);
            transform.Find("AreaGravidade").gameObject.SetActive(false);
            transform.Find("AreaPousoEmergencia").gameObject.SetActive(false);
            transform.Find("AreaAsteroides").gameObject.SetActive(false);
        }
    }

    public void HabilitarAreaGravidade()
    {
        GameObject area = transform.Find("AreaGravidade").gameObject;
        if(area.activeSelf == false)
        {
            area.SetActive(true);
            transform.Find("AreaPrincipal").gameObject.SetActive(false);
            transform.Find("AreaPousoEmergencia").gameObject.SetActive(false);
            transform.Find("AreaAsteroides").gameObject.SetActive(false);
        }
    }

    public void HabilitarAreaPousoEmergencia()
    {
        GameObject area = transform.Find("AreaPousoEmergencia").gameObject;
        if(area.activeSelf == false)
        {
            area.SetActive(true);
            transform.Find("AreaGravidade").gameObject.SetActive(false);
            transform.Find("AreaPrincipal").gameObject.SetActive(false);
            transform.Find("AreaAsteroides").gameObject.SetActive(false);
        }
    }

    public void HabilitarAreaAsteroides()
    {
        GameObject area = transform.Find("AreaAsteroides").gameObject;
        if(area.activeSelf == false)
        {
            area.SetActive(true);
            transform.Find("AreaGravidade").gameObject.SetActive(false);
            transform.Find("AreaPousoEmergencia").gameObject.SetActive(false);
            transform.Find("AreaPrincipal").gameObject.SetActive(false);
        }
    }
}
