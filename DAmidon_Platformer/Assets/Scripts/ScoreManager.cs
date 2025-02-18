using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;
    public int score;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = $"Fish Count {score}";
    }
}
