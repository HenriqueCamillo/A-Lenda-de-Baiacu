using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual
{
    public float minDist;
    public float maxDist;
    public float fitness;
    private float normie = 10f;

    public Individual(){}
    public Individual(float minDist, float maxDist, int score, Vector2 finalPos, Vector2 wallPos)
    {
        this.minDist = minDist;
        this.maxDist = maxDist;

        fitness = score + (1 - Vector2.Distance(finalPos, wallPos) / normie);        
    }


}
