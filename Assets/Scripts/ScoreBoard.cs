using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public static ScoreBoard instance;
    private int score;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    /// <summary>
    /// Singleton
    /// </summary>
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        Score = 0;
    }

}
