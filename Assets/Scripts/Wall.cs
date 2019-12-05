using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Rigidbody2D rBody;
    [SerializeField] float speed;
    private bool hasBeenPassed = false;

    [SerializeField] GameObject upperWall;
    [SerializeField] GameObject lowerWall;


    /// <summary>
    /// Inicia seu movimento para a esquerda
    /// </summary>
    void Start()
    {
        rBody.velocity = Vector2.left * speed;
    }

    /// <summary>
    /// Verifica se o player passou pela parede, contabilizando um ponto caso tenha passado.
    /// </summary>
    void Update()
    {
        if (!hasBeenPassed && GameManager.instance.playerStartPosition.position.x > this.transform.position.x)
        {
            hasBeenPassed = true;
            ScoreBoard.instance.Score++;
        }   
       
    }

    /// <summary>
    /// Calcula as distâncias da parede para o player e retorna
    /// </summary>
    /// <returns>Distâncias da parede para o player</returns>
    public Distances GetDistances(Transform player)
    {
        Distances distances = new Distances();

        distances.horizontalDistance = player.position.x - this.transform.position.x;
        distances.upperWallDistance  = player.position.y - upperWall.transform.position.y;
        distances.lowerWallDistance  = player.position.y - lowerWall.transform.position.y;

        return distances;
    }
}