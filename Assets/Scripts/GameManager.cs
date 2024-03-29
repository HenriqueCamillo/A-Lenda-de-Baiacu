﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Tooltip("Este modo deve estar habilitado caso o o modo de jogo evolutivo esteja ativado")]
    [SerializeField] bool automaticMode;
    public Player player;
    [SerializeField] Spawner spawner;
    [SerializeField] Background background;
    [SerializeField] GameObject menu;
    public Transform playerStartPosition;


    /// <summary>
    /// Singleton
    /// </summary>
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    /// <summary>
    /// Escolhe a função de game over a ser chamada dependendo do modo de jogo
    /// </summary>
    public void GameOver()
    {
        if (automaticMode)
            Reset();
        else
            StopAndShowMenu();
    }

    /// <summary>
    /// Pausa o jogo e mostra o menu
    /// </summary>
    void StopAndShowMenu()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
    }

    /// <summary>
    /// Reseta a cena toda
    /// </summary>
    public void PlayAgain()
    {
        player.Reset();
        spawner.Reset();
        background.Reset();
        ScoreBoard.instance.Score = 0;
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Reset do modo evolutivo (em que não há menu, jogador e time scale para resetar)
    /// </summary>
    void Reset()
    {
        spawner.Reset();
        background.Reset();
        ScoreBoard.instance.Score = 0;
    }

    /// <summary>
    /// Carrega o menu principal
    /// </summary>
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}