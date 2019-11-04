using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Rigidbody2D rBody;
    [SerializeField] float speed;
    private bool hasBeenPassed = false;
    public Transform player;

    [SerializeField] GameObject upperWall;
    [SerializeField] GameObject lowerWall;


    /// <summary>
    /// Pega referência do jogador e do rigidbody, e inicia seu movimento para a esquerda
    /// </summary>
    void Start()
    {
        player = GameManager.instance.player.transform;
        rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = Vector2.left * speed;

    }

    /// <summary>
    /// Verifica se o player passou pela parede, contabilizando um ponto caso tenha.
    /// </summary>
    void Update()
    {
        if (!hasBeenPassed && player.transform.position.x > this.transform.position.x)
        {
            hasBeenPassed = true;
            ScoreBoard.instance.Score++;
        }
    }

    /// <summary>
    /// Calcula as distâncias da parede para o player e retorna
    /// </summary>
    /// <returns>Distâncias da parede para o player</returns>
    public Distances GetDistances()
    {
        Distances distances = new Distances();

        distances.horizontalDistance = Mathf.Abs(player.transform.position.x - this.transform.position.x);
        distances.upperWallDistance  = Mathf.Abs(player.transform.position.y - upperWall.transform.position.y);
        distances.lowerWallDistance  = Mathf.Abs(player.transform.position.y - lowerWall.transform.position.y);

        return distances;
    }

    /// <summary>
    /// Destroi parede depois que ela sai da tela
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
