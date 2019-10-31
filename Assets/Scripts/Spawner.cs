using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] float distanceBetweenWalls;
    [SerializeField] Transform limitUp;
    [SerializeField] Transform limitDown;
    private Transform lastWall;

    /// <summary>
    /// Inicia o spawner
    /// </summary>
    void Start()
    {
        SpawnWall();  
    }

    /// <summary>
    /// Verifica se a última parede criada já está longe o suficiente, para então criar outra
    /// </summary>
    void Update()
    {
        if (lastWall != null)
            if (Mathf.Abs(this.transform.position.x - lastWall.transform.position.x) >= distanceBetweenWalls)
                SpawnWall();
    }

    /// <summary>
    /// Cria uma parede com altura aleatória, e pega uma referência a ela
    /// </summary>
    void SpawnWall()
    {
        //float height = Random.Range(limitDown.position.y, limitUp.position.y); 
        float height = limitUp.position.y; 
        Debug.Log(height);
        Vector2 position = new Vector2(this.transform.position.x, height);
        GameObject instance = Instantiate(wall, position, Quaternion.identity, this.transform);
        lastWall = instance.transform;
    }

    /// <summary>
    /// Destroi todas as paredes e recomeça o spawner
    /// </summary>
    public void Reset()
    {
        foreach (Transform child in this.transform)
            Destroy(child.gameObject);

        Start();
    }
}
