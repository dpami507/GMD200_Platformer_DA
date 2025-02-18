using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private void Update()
    {
        GetComponent<AudioSource>().volume = GameManager.instance.volume;
    }
}
