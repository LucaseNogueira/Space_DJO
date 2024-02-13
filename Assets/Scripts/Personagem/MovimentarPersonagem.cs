using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPersonagem : MonoBehaviour
{
    public CharacterController controle;
    public float velocidade = 6f;
    
    //Para Andar
    public Transform checaChao;
    public float raioEsfera = 0.4f;
    public LayerMask chaoMask;
    public bool estaNoChao;
    public float alturaPulo = 0f;

    //Para Acao da Gravidade
    public float forcaGravidade = -20f;
    Vector3 velocidadeCai;

    private bool congelar;

    // Start is called before the first frame update
    void Start()
    {
        congelar = false;
        controle = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics.CheckSphere(checaChao.position, raioEsfera, chaoMask);

        Andar();
        Pular();
        Gravidade();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(checaChao.position, raioEsfera);
    }

    public void CongelarPersonagem()
    {
        congelar = true;
    }

    public void DescongelarPersonagem()
    {
        congelar = false;
    }

    public bool GetCongelarPersonagem()
    {
        return congelar;
    }

    private void Andar()
    {
        if(congelar == false)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 mover = transform.right * x + transform.forward * z;

            controle.Move(mover * velocidade * Time.deltaTime);
        }
    }

    private void Pular()
    {
        if(estaNoChao                   &&
           Input.GetButtonDown("Jump")  &&
           alturaPulo != 0f             &&
           congelar == false)
        {
            velocidadeCai.y = Mathf.Sqrt(alturaPulo * -2f * forcaGravidade);
        }
    }

    private void Gravidade()
    {
        if(!estaNoChao){
            //Altera a velocidade de queda para o personagem cair
            velocidadeCai.y += forcaGravidade * Time.deltaTime;
        }

        //Exerce a forca da gravidade no personagem
        controle.Move(velocidadeCai * Time.deltaTime);
    }
}
