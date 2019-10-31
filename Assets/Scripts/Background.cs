using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Transform root;
    private Vector2 startPosition;
    [SerializeField] float speed;

    void Start()
    {
        root = this.transform.parent;
        startPosition = root.transform.position;
    }

    void Update()
    {
        root.transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Reset();
    }

    public void Reset()
    {
        root.position = startPosition;
    }
}