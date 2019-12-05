using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PopulationManager : MonoBehaviour
{

    //tamanhp da população
    [SerializeField] int popSize;

    //taxa para ocorrer o crossover
    [SerializeField] float crossoverRate;
    
    //taxa para ocorrer a mutação
    [SerializeField] float mutationRate; 
    [SerializeField] float[] mutationRange;


    //valores iniciais [min, max] para minDist e maxDist
    [SerializeField] float[] minDistRange;
    [SerializeField] float[] maxDistRange;
    [Tooltip("Use esta variável para deixar os testes mais rápidos")]
    [SerializeField] float timeScale;

    [Tooltip("Prefab do peixe já no modo automático")]
    [SerializeField] GameObject playerPrefab;
    // Quantidade de indivíduos jogando no momento


    //lista da população
    private List<Individual> population = new List<Individual>();
    public static PopulationManager instance;
  
    //quantidade de gerações
    [SerializeField] int generation = 0;
    [SerializeField] TextMeshProUGUI generationText;

    /// <summary>
    /// Ao atualizar a geração, atualiza a UI também
    /// </summary>
    /// <returns></returns>
    private int Generation
    {
        get => generation;
        set
        {
            generation = value;
            generationText.text = "Geração " + generation.ToString();

        }
    }

    /// <summary>
    /// Singleton
    /// Cria primeira geração
    /// </summary>
    void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(this.gameObject);

        if (timeScale != 0)
            Time.timeScale = timeScale;

        CreateNewGeneration();        
    }

    /// <summary>
    /// seleciona parte da população que possui fitness maior ou igual a média dos fitness
    /// </summary>
    /// <returns></returns>
    List<Individual> Selection()
    {
        List<Individual> newPop = new List<Individual>();
        
        //ordena a lista em ordem decrescente de fitness
        List<Individual> pop = population;
        pop.Sort((ind1,ind2)=> -1 * ind1.fitness.CompareTo(ind2.fitness));

        //calcula a média dos fitness
        float averageFitness =  population.Average(ind=>ind.fitness);

        //pega os indivíduos que tem fitness maior ou igual
        for(int i = 0; i < pop.Count; i++)
        {
            // O mínimo de índividuos da nova popoulação é 2, para que seja possível
            // fazer o crossover, então os dois primeiros indivíduos são selecionados
            if (i < 2)
                newPop.Add(pop[i]);
            // Seleciona no máximo dois terços da população atual
            else if (newPop.Count >= (float)pop.Count * 2f/3f)
                break;
            else if(pop[i].fitness >= averageFitness)
                newPop.Add(pop[i]);
            else
                break;
        }

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

                    if (descendants.Count < popSize - selectedPop.Count)
                    {
                        Individual ind2 = new Individual();

                        ind2.minDist = selectedPop[index2].minDist;
                        ind2.maxDist = selectedPop[index1].maxDist;
                        descendants.Add(ind2);
                    }

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
                    descendants[i].minDist += Random.Range(mutationRange[0], mutationRange[1]);
                else
                    descendants[i].maxDist += Random.Range(mutationRange[0], mutationRange[1]);
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
        
        Generation++;
    }

    /// <summary>
    /// Quando todos os peixes perdem o jogo, uma nova geração é gerada
    /// Verificação a todo frame pois o uso de uma variável de contagem gera bugs quando vários peixes morrem junto
    /// </summary>
    void Update()
    {
        if (this.transform.childCount == 0)
        {
            Debug.Log("Gen " + generation + " max score: " + ScoreBoard.instance.Score);
            CreateNewGeneration();
        }

    }

    /// <summary>
    /// Atualiza o fitnesse de um inidivíduo dado seu index e seu novo fitness
    /// </summary>
    /// <param name="index">Index do indivíduo na lista</param>
    /// <param name="fitness">Novo fitness</param>
    public void UpdateFitness(int index, int score, float fitness)
    {
        population[index].fitness = fitness;
        RegisterResults(index, score, fitness);
    }

    /// <summary>
    /// Registra o desempenho do indivíduo
    /// </summary>
    void RegisterResults(int index, int score, float fitness)
    {
        //TODO implementar registro dos indivíduos
        // Debug.Log("Score: " + score + "\nFitness: " + fitness + "\nGenes: " + population[index].minDist + " " + population[index].maxDist);
    }

    /// <summary>
    /// Registra a população antes de ela jogar
    /// </summary>
    void RegisterPopulation()
    {
        //TODO implementar registro das populações
    }

    /// <summary>
    /// Gera nova população, registra-a, e spawna os indivíduos para testá-los
    /// </summary>
    void CreateNewGeneration()
    {
        if (generation > 0)
            GameManager.instance.GameOver();
        Generator();
        RegisterPopulation();
        SpawnPopulation();
    }

    /// <summary>
    /// Spawna a população inteira
    /// </summary>
    void SpawnPopulation()
    {
        for (int i = 0; i < population.Count; i++)
        {
            GameObject player = Instantiate(playerPrefab, GameManager.instance.playerStartPosition.position, Quaternion.identity, this.transform);
            player.GetComponent<Player>().Initialize(i, population[i].minDist, population[i].maxDist);
        }
    }

    /// <summary>
    /// Volta ao menu
    /// //TODO salvar os resultados dos que ainda estão em jogo
    /// </summary>
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
