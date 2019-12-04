using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{

    //tamanhp da população
    [SerializeField] int popSize;

    //taxa para ocorrer o crossover
    [SerializeField] float crossoverRate = 0.8f;
    
    //taxa para ocorrer a mutação
    [SerializeField] float mutationRate = 0.05f; 

    //quantidade de gerações
    [SerializeField] int generation;

    //valores iniciais [min, max] para minDist e maxDist
    [SerializeField] float[] minDistRange = {0f, 5f};
    [SerializeField] float[] maxDistRange = {0f, 5f};

    //lista da população
    public List<Individual> population = new List<Individual>();
    public static PopulationManager instance;
  
    /// <summary>
    /// Singleton
    /// </summary>
    void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(this.gameObject);
    }

    /// <summary>
    /// seleciona parte da população que possui fitness maior ou igual a média dos fitness
    /// </summary>
    /// <returns></returns>
    List<Individual> Selection()
    {
        List<Individual> newPop = new List<Individual>();

        //calcula a média dos fitness
        float averageFitness =  population.Average(ind=>ind.fitness);

        //pega os indivíduos que tem fitness maior ou igual
        foreach(Individual ind in population)
        {
            if(ind.fitness >= averageFitness)
            {
                newPop.Add(ind);
            }
        }

        //ordena a lista em ordem decrescente de fitness
        newPop.Sort((ind1,ind2)=> -1 * ind1.fitness.CompareTo(ind2.fitness));

        return newPop;
    }

    /// <summary>
    /// Aplica o crossover nos indivíduos selecionados, encontrando a nova população
    /// </summary>
    /// <param name="selectedPop"> lista com os indivíduos selecionados</param>
    /// <returns></returns>
    List<Individual> Crossover(List<Individual> selectedPop)
    {
        List<Individual> descendants = new List<Individual>();

        //enquanto o tamanho da lista de descendentes for menor do que o que falta para completar a população
        while(descendants.Count < popSize - selectedPop.Count)
        {
            //pega dois pais aleatoriamente entre os indivíduos selecionados
            int index1 = Random.Range(0, selectedPop.Count);
            int index2 = Random.Range(0, selectedPop.Count);

            //se os pais forem diferentes
            if(index1 != index2)
            {
                //se o valor for maior que o crossoverRate, não ocorre crossover
                if(Random.Range(0f, 1f) > crossoverRate)
                {
                    //apenas pega os pais e coloca na lista de descendentes
                    descendants.Add(selectedPop[index1]);
                    descendants.Add(selectedPop[index2]);
                }
                //se ocorrer o crossover
                else
                {
                    //cada indivíduo tem metade de uma pai e a outra metade de outro
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

    /// <summary>
    /// Aplica mutação nos descendentes
    /// </summary>
    /// <param name="descendants">lista de descendentes</param>
    void Mutation(List<Individual> descendants)
    {
        for(int i = 0; i < descendants.Count; i++)
        {
            //se puder ocorrer mutação
            if(Random.Range(0f, 1f) < mutationRate)
            {
                //decido se vai ocorrer mutação no gene do minDist ou no gene do maxDist
                if(Random.Range(0, 2) == 0)
                    descendants[i].minDist += Random.Range(-0.25f, 0.25f);
                else
                    descendants[i].maxDist += Random.Range(-0.25f, 0.25f);
            }
        }
    }

    /// <summary>
    /// Função geradora das populações
    /// </summary>
    public void Generator()
    {
        //cria a primeira população
        if(generation == 0)
        {
            //cria cada indivíduo da primeira população com valores aleatórios nos genes
            for(int i = 0; i < popSize; i++)
            {
                Individual ind = new Individual();
                ind.minDist = Random.Range(minDistRange[0], maxDistRange[1]);
                ind.maxDist = Random.Range(maxDistRange[0], maxDistRange[1]);
                population.Add(ind);
            }
        }
        //se já existe uma ou mais gerações, aplicamos a evolução
        else
        {
            //realiza a seleção
            List<Individual> selectedPop = Selection();
            //faz o crossover para encontrar os descendentes
            List<Individual> descendants = Crossover(selectedPop);
            //aplica a mutação nos descendentes
            Mutation(descendants);

            //a nova população vira a junção dos selecionados mais os descendentes
            population = selectedPop.Concat(descendants).ToList();

        }
            generation++;
    }
}
