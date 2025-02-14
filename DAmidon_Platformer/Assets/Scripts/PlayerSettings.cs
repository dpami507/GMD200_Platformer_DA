using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Platformer/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Movement")]
    public float moveForce;
    public float counterMovement;
    public float maxSpeed;

    [Header("Jump")]
    public int jumpsCount;
    public float jumpForce;
    public GameObject jumpParticle;

    [Header("Ground Check")]
    public float castRadius;
    public LayerMask castMask;
}
