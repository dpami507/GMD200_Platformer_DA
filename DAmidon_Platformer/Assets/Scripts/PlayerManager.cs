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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Kill"))
        {
            Die();
        }
    }
}
