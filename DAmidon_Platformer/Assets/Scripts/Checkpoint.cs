using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    LevelManager levelManager;
    public Color unclaimedColor, claimedColor;
    public ParticleSystem collectParticles;
    public bool claimed;
    public bool lastClaimed;
    public int id;

    private void Start()
    {
        lastClaimed = claimed;
        levelManager = FindObjectOfType<LevelManager>();
        GetComponent<SpriteRenderer>().color = unclaimedColor;
    }

    private void Update()
    {
        if (lastClaimed != claimed) 
        {
            lastClaimed = claimed;
            collectParticles.Play();
            GetComponent<SpriteRenderer>().color = claimedColor;
        }
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
