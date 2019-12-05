using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// Entra no modo de jogo manual
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Entra no modo de jogo automátivo, o evolutivo
    /// </summary>
    public void EvolutiveMode()
    {
        SceneManager.LoadScene("EvolutiveMode");
    }
}