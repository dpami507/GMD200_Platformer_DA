using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject fishExplosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ScoreManager.instance.score++;
            Destroy(Instantiate(fishExplosion, transform.position, Quaternion.identity), 2f);
            Destroy(this.gameObject);
        }
    }
}
