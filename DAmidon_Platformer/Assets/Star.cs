using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float waveSpeed;
    public float waveHeight;
    public Transform sprite;
    float currentWaveY;

    private void Update()
    {
        currentWaveY = (waveHeight * Mathf.Sin(Time.time * waveSpeed));

        sprite.localPosition = new Vector3(0, currentWaveY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.Instance.starsCollected++;
            Destroy(this.gameObject);
        }
    }
}
