using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Platformer/Camera Settings")]
public class CameraSettings : ScriptableObject
{
    public Vector3 offset;
    public float lerpSpeed;
}
