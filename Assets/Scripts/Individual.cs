using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual
{
    //gene 1
    public float minDist;

    //gene 2
    public float maxDist;
    public float fitness;

    //valor para normalizar a distancia
    private static float normalizer = 10f;

    public static float Fitness(float score, Vector2 finalPos, Vector2 wallPos)
    {
        return score + (1 - Vector2.Distance(finalPos, wallPos) / normalizer);        
    }


}
