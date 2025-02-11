using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    LevelManager levelManager;
    public Color unclaimedColor, claimedColor;
    public ParticleSystem collectParticles;
    public bool claimed;
    public int id;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        GetComponent<SpriteRenderer>().color = unclaimedColor;
    }

    void ClaimPoint()
    {
        if(!claimed)
        {
            levelManager.SetCurrentCheckpoint(id);
            collectParticles.Play();
            GetComponent<SpriteRenderer>().color = claimedColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ClaimPoint();
        }
    }
}
