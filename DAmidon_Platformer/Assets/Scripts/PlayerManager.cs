using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void Die()
    {
        transform.position = levelManager.mostRecentCheckPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Kill"))
        {
            Die();
        }
    }
}
