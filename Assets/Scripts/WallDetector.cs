using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    [SerializeField] Player player;
    
    [Tooltip("Mínima distância vertical para o topo dos corais inferiores. Para valores menores que este, o peixe irá inflar")]
    public  float minDist;

    [Tooltip("Máxima distância horizontal para o centro dos corais. Para valores maiores que este, o peixe ignorará essa parede e começcara a considerar a próxima")]
    public float maxDist;

    // Referência da parede, e variável para salvar suas distâncias para o jogador
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
            distances = wall.GetDistances(player.transform);

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
        Debug.DrawRay(this.transform.position, Vector2.right * 3, Color.green, 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right);
        if (hit)
        {
            wall = hit.transform.GetComponent<Wall>();
            distances = wall.GetDistances(player.transform);
        }        
    }

    /// <summary>
    /// Detecta a parede mais próxima que o baiacu ainda não passou
    /// </summary>
    /// <returns>Parede mais próxima que o baiacu ainda não passou </returns>
    public Transform GetNextWallTransform()
    {
        // Verifica se a refência da parede que temos é a correta, retornando-a, caso verdadeiro
        if (wall.transform.position.x > this.transform.position.x)
            return wall.transform;
        // Caso contrário, usa um raycast para procurar a próxima parede
        else
        {
            Debug.DrawRay(this.transform.position, Vector2.right * 3, Color.green, 0.1f);

            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right);
            if (hit)
                return hit.transform;
            // Caso não tenha enconrado nada no raycast, utiliza a referência da parede já possuída
            else   
                return wall.transform;
        }
    }
}