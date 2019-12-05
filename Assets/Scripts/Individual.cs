using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual
{
    //gene 1
    public float minDist;

    //gene 2
    public float maxDist;
    public float fitness = 0;

    //valor para normalizar a distancia
    private static float normalizer = 10f;

    /// <summary>
    /// Função que calcula o fitness baseado na pontuação e no quão perto de fazer o próximo ponto o peixe chegou
    /// O retornado é a pontuação somada a uma valor entre 0 e 1, baseado na distância para o centro da próxima parede
    /// </summary>
    /// <param name="score">Pontuação final</param>
    /// <param name="finalPos">Posição final</param>
    /// <param name="wallPos">Posição da próxima parede</param>
    /// <returns>Valor do fitness</returns>
    public static float Fitness(float score, Vector2 finalPos, Vector2 wallPos)
    {
        return score + (1 - Vector2.Distance(finalPos, wallPos) / normalizer);        
    }


}
