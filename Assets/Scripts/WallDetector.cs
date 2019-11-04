using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    [SerializeField] float rayLenght;

    // Update is called once per frame
    void Update()
    {
        CastRay();

    }

    /// <summary>
    /// Utiliza um raycast para detectar paredes e pegar as distâncias horizontal e vertical do baiacu até ela
    /// </summary>
    void CastRay()
    {
        Debug.DrawRay(this.transform.position, Vector2.right * rayLenght, Color.green, 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right, rayLenght);
        if (hit)
        {
            Distances distances = hit.transform.GetComponent<Wall>().GetDistances();
            //TODO fazer os cálculos necessários com as distâncias
        }
    }

}