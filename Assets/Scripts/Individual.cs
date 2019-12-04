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
    private float normie = 10f;

    public Individual(){}
    public Individual(float minDist, float maxDist, int score, Vector2 finalPos, Vector2 wallPos)
    {
        this.minDist = minDist;
        this.maxDist = maxDist;

        //cálculo do fitness do indivíduo
        fitness = score + (1 - Vector2.Distance(finalPos, wallPos) / normie);        
    }


}
