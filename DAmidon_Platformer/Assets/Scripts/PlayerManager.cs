using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    LevelManager levelManager;
    public bool dead;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void Die()
    {
        Invoke(nameof(LateDie), 1f);
        SoundManager.instance.PlaySound("Death");
        dead = true;
    }

    public void LateDie()
    {
        transform.position = levelManager.mostRecentCheckPoint;
        dead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Kill"))
        {
            Die();
        }
    }
}
