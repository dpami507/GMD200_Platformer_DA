using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Animator animator;
    LevelManager levelManager;

    public ParticleSystem collectParticles;
    public GameObject flameLight;
    public AudioSource fireStart;
    public AudioSource fireCrackle;

    public bool claimed;
    bool lastClaimed;
    public int id;

    private void Start()
    {
        lastClaimed = claimed;
        levelManager = FindObjectOfType<LevelManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //if claimed changes play style features
        if (lastClaimed != claimed) 
        {
            lastClaimed = claimed;
            collectParticles.Play();
            fireStart.Play();
            fireCrackle.Play();
        }

        animator.SetBool("claimed", claimed);
        flameLight.SetActive(claimed);
    }

    void ClaimPoint()
    {
        //claim checkpoint and update level manager
        if(!claimed)
        {
            levelManager.SetCurrentCheckpoint(id);
            collectParticles.Play();
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
