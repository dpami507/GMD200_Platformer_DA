using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : PlayerInteractable
{
    public bool playerOverSign;
    public GameObject signText;
    BoxCollider2D checkCollider;

    private void Start()
    {
        checkCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Collider2D[] colliders = GetCollidersInBox(transform.position + (Vector3)checkCollider.offset, checkCollider.size);

        playerOverSign = TagInArray(colliders, "Player");

        signText.SetActive(playerOverSign);
    }
}
