using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rBody;
    [SerializeField] bool automaticMode;
    [SerializeField] float impulseForce;
    private Vector2 startPosition;

    [SerializeField] Animator anim;
    /// <summary>
    /// Pega referência do Rigidbody e salva sua posição inicial
    /// </summary>
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();        
        anim = GetComponent<Animator>();
        startPosition = this.transform.position;
    }

    /// <summary>
    /// Verifica a todo momento se houve input do jogador. Caso haja, infla o peixe.
    /// </summary>
    void Update()
    {
        if (InflateInputPressed())
        {
            anim.Play("Movement", 0);
            Inflate();
        }

    }

    /// <summary>
    /// Verifica se foi apertado algum botão usado para inflar o peixe
    /// </summary>
    /// <returns>Verdadeiro se foi detectado input, falso caso contrário.</returns>
    bool InflateInputPressed()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) 
            return true;
        else
            return false;
    }

    /// <summary>
    /// Dá um impulso para cima e //TODO ativa animação de inflar.
    /// </summary>
    public void Inflate()
    {
        rBody.velocity = Vector3.zero;
        rBody.AddForce(Vector2.up * impulseForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Reposiciona o jogador para sua posição inicial
    /// </summary>
    public void Reset()
    {
        this.transform.position = startPosition;
    }


    /// <summary>
    /// É dado game over quando o jogador bate em alguma coisa
    /// </summary>
    /// <param name="_"></param>
    void OnTriggerEnter2D(Collider2D _)
    {
        GameManager.instance.GameOver();
    }
}