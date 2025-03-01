using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit Dani - YouTube : https://www.youtube.com/watch?v=zit45k6CUMk
public class Parallax : MonoBehaviour
{
    float length, startPos;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;// * transform.localScale.x; 
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
