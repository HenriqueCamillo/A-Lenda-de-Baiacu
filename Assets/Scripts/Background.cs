using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Transform root;
    private Vector2 startPosition;
    [SerializeField] float speed;

    /// <summary>
    /// Salva posição inicial dos backgrounds
    /// </summary>
    void Start()
    {
        root = this.transform.parent;
        startPosition = root.transform.position;
    }

    /// <summary>
    /// Move os backgrounds
    /// </summary>
    void Update()
    {
        root.transform.position += Vector3.left * speed * Time.deltaTime;
    }

    /// <summary>
    /// Reseta os backgrouns quando o primeiro background não é mais visto na tela
    /// </summary>
    void OnBecameInvisible()
    {
        Reset();
    }

    /// <summary>
    /// Reseta o conjunto de backgrounds para sua posição inicial
    /// </summary>
    public void Reset()
    {
        root.position = startPosition;
    }
}