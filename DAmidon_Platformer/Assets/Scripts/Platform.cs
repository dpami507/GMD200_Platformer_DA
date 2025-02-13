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
        float t = (Mathf.Sin(Time.time * moveSpeed) + 1) / 2;
        Vector2 currentPos = new Vector2(pos1.position.x - (pos1.position.x - pos2.position.x) * t, pos1.position.y - (pos1.position.y - pos2.position.y) * t);
        platform.transform.position = currentPos;
    }
}
