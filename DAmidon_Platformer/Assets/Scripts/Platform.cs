using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float moveSpeed;
    public Transform pos1;
    public Transform pos2;

    public Transform platform;

    private void FixedUpdate()
    {
        //Create Sin (0-1) based on speed
        float t = (Mathf.Sin(Time.time * moveSpeed) + 1) / 2;

        //OMG USE ONE OF YOUR CLASSES!!!!!!!!! (Parametric Equations)
        Vector2 vector = pos2.position - pos1.position;
        Vector2 currentPos = new Vector2(pos1.position.x + (vector.x * t), pos1.position.y + (vector.y * t));
        platform.transform.position = currentPos;
    }
}
