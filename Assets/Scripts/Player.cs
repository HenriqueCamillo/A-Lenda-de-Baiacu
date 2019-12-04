using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rBody;
    [SerializeField] Animator anim;
    [SerializeField] WallDetector wallDetector;
    public bool automaticMode;
    [SerializeField] float impulseForce;
    private int index;
    public int score = 0;


    /// <summary>
    /// Pega referência do Rigidbody e do Animator e salva sua posição inicial
    /// </summary>
    void Start()
    {
        // rBody = GetComponent<Rigidbody2D>();        
        // anim = GetComponent<Animator>();
        // startPosition = this.transform.position;
    }


    public void Initialize(int index, float minDist, float maxDist)
    {
        this.index = index;
        wallDetector.SetGenes(minDist, maxDist);
    }

    /// <summary>
    /// Verifica a todo momento se houve input do jogador. Caso haja, infla o peixe.
    /// </summary>
    void Update()
    {
        if (!automaticMode && InflateInputPressed())
            Inflate();

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
    /// Dá um impulso para cima e ativa animação de inflar.
    /// </summary>
    public void Inflate()
    {
        rBody.velocity = Vector3.zero;
        rBody.AddForce(Vector2.up * impulseForce, ForceMode2D.Impulse);

        anim.Play("Movement", 0);
    }

    /// <summary>
    /// Reposiciona o jogador para sua posição inicial
    /// </summary>
    public void Reset()
    {
        this.transform.position = GameManager.instance.playerStartPosition.position;
        rBody.velocity = Vector2.zero;
    }


    /// <summary>
    /// É dado game over quando o jogador bate em alguma coisa
    /// </summary>
    /// <param name="_"></param>
    void OnTriggerEnter2D(Collider2D _)
    {
        // Se estiver no modo automático, cria uma Ind
        if (automaticMode)
        {
            score = ScoreBoard.instance.Score;  
            Transform wall = wallDetector.GetNextWallTransform();
            float fitness = Individual.Fitness(score, this.transform.position, wall.position);
            PopulationManager.instance.UpdateFitness(index, score, fitness);

            Destroy(this.gameObject);
        }
        else
            GameManager.instance.GameOver();
    }
}