using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    /// <summary>
    /// Destroi tudo que o toca
    /// </summary>
    /// <param name="other">O objeto com que houve a colisão</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
