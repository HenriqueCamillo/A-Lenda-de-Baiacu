using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float rayLenght;
    [SerializeField] float minDist;
    [SerializeField] float maxDist;
    private Wall wall;
    private Distances distances;

    /// <summary>
    /// Define os genes do indivíduo
    /// </summary>
    /// <param name="minDist"></param>
    /// <param name="maxDist"></param>
    public void SetGenes(float minDist, float maxDist)
    {
        this.minDist = minDist;
        this.maxDist = maxDist;
    }

    /// <summary>
    /// Lida com a movimentação do baiacu
    /// </summary>
    void Update()
    {
        // Se a parede não for nula
        if (wall != null)
        {
            distances = wall.GetDistances();

            // Verifica se ele já está longe o suficiente dos corais para fazer os cálculos para os próximos corais            
            if (distances.horizontalDistance > maxDist)
                GetNextWall();

            // Verifica se é necessário inflar o baiacu para ele não cair nos corais
            if (distances.lowerWallDistance < minDist) 
                player.Inflate();
                
        }
        // Caso a parede seja nula, pega referência da próxima parede
        else
            GetNextWall();

        
    }

    /// <summary>
    /// Utiliza um raycast para detectar paredes e pegar as distâncias horizontal e vertical do baiacu até ela
    /// </summary>
    void GetNextWall()
    {
        Debug.DrawRay(this.transform.position, Vector2.right * rayLenght, Color.green, 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right, rayLenght + 10);
        if (hit != null)
        {
            wall = hit.transform.GetComponent<Wall>();
            distances = wall.GetDistances();
        }
    }
}