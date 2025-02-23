using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Checkpoint[] checkpoints;
    public Vector2 mostRecentCheckPoint;

    public static LevelManager Instance;

    Transform player;
    public Transform startPos;

    private void OnEnable()
    {
        player = FindFirstObjectByType<PlayerManager>().transform;
        player.position = startPos.position;
    }

    private void Start()
    {
        Instance = this;

        if(checkpoints.Length > 0)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                checkpoints[i].id = i + 1;
            }

            //Set checkpoint to first when starting
            mostRecentCheckPoint = checkpoints[0].transform.position;
        }
    }

    //Claim point
    public void SetCurrentCheckpoint(int id)
    {
        //Loop though to make sure all prior points are claimed
        for (int i = 0; i < id; i++)
        {
            Debug.Log($"Claimed point {id}");
            checkpoints[i].claimed = true;
            mostRecentCheckPoint = checkpoints[i].transform.position;
        }
    }
}
