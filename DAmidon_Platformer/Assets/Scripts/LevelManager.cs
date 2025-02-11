using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int maxStars;
    public int starsCollected;

    public Checkpoint[] checkpoints;
    public Vector2 mostRecentCheckPoint;

    public TMP_Text starCounterText;

    public static LevelManager Instance;

    private void Start()
    {
        Instance = this;
        maxStars = FindObjectsOfType<Star>().Length;

        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].id = i + 1;
        }
    }

    private void Update()
    {
        starCounterText.text = $"{starsCollected} / {maxStars}";
    }

    public void SetCurrentCheckpoint(int id)
    {
        for (int i = 0; i < id; i++)
        {
            Debug.Log($"Claimed point {id}");
            checkpoints[i].claimed = true;
            mostRecentCheckPoint = checkpoints[i].transform.position;
        }
    }
}
