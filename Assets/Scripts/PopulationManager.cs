using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] int popSize;
    [SerializeField] float crossoverRate = 0.8f;
    [SerializeField] float mutationRate = 0.05f; 
    [SerializeField] int generation;
    [SerializeField] float[] minDistRange = {0f, 5f};
    [SerializeField] float[] maxDistRange = {0f, 5f};
    public static PopulationManager instance;
    List<Individual> population = new List<Individual>();
  
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

  
    void Update()
    {
        
    }

    List<Individual> Selection()
    {
        List<Individual> newPop = new List<Individual>();

        float averageFitness =  population.Average(ind=>ind.fitness);

        foreach(Individual ind in population)
        {
            if(ind.fitness >= averageFitness)
            {
                newPop.Add(ind);
            }
        }

        newPop.Sort((ind1,ind2)=> -1 * ind1.fitness.CompareTo(ind2.fitness));

        return newPop;
    }

    List<Individual> Crossover(List<Individual> selectedPop)
    {
        List<Individual> descendants = new List<Individual>();

        while(descendants.Count < popSize - selectedPop.Count)
        {

            int index1 = Random.Range(0, selectedPop.Count);
            int index2 = Random.Range(0, selectedPop.Count);

            if(index1 != index2)
            {
                if(Random.Range(0f, 1f) > crossoverRate)
                {
                    descendants.Add(selectedPop[index1]);
                    descendants.Add(selectedPop[index2]);
                }
                else
                {
                    Individual ind1 = new Individual();

                    ind1.minDist = selectedPop[index1].minDist;
                    ind1.maxDist = selectedPop[index2].maxDist;
                    descendants.Add(ind1);

                    Individual ind2 = new Individual();

                    ind2.minDist = selectedPop[index2].minDist;
                    ind2.maxDist = selectedPop[index1].maxDist;
                    descendants.Add(ind2);

                }
            }

        }
        
        return descendants;
    }

    void Mutation(List<Individual> descendants)
    {
        for(int i = 0; i < descendants.Count; i++)
        {
            if(Random.Range(0f, 1f) < mutationRate)
            {
                if(Random.Range(0, 2) == 0)
                    descendants[i].minDist += Random.Range(-0.25f, 0.25f);
                else
                    descendants[i].maxDist += Random.Range(-0.25f, 0.25f);
            }
        }
    }

    void Generator()
    {
        if(generation == 0)
        {
            for(int i = 0; i < popSize; i++)
            {
                Individual ind = new Individual();
                ind.minDist = Random.Range(minDistRange[0], maxDistRange[1]);
                ind.maxDist = Random.Range(maxDistRange[0], maxDistRange[1]);
                population.Add(ind);
            }
        }
        else
        {
            List<Individual> selectedPop = Selection();
            List<Individual> descendants = Crossover(selectedPop);
            Mutation(descendants);

            population = selectedPop.Concat(descendants).ToList();

        }
            generation++;
    }
}
