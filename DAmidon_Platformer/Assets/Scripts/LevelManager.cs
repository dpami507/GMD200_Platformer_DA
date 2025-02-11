using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Checkpoint[] checkpoints;
    public Vector2 mostRecentCheckPoint;

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
