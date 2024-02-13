using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoCamera : MonoBehaviour
{

    public float sensibilidadeMouse = 100f;
    public float anguloMinimo = -90f;
    public float anguloMaximo = 90f;
    float rotacao = 0f;

    public Transform transformPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotacionar();
    }

    private void Rotacionar()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse * Time.deltaTime;

        rotacao -= mouseY;
        rotacao = Mathf.Clamp(rotacao, anguloMinimo, anguloMaximo);
        //Para cima e para baixo
        transform.localRotation = Quaternion.Euler(rotacao, 0, 0);
        //Pro lado e pro outro
        transformPlayer.Rotate(Vector3.up * mouseX);
    }
}
