using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
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
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)checkCollider.offset, checkCollider.size, 0);

        playerOverSign = TagInArray(colliders, "Player");

        signText.SetActive(playerOverSign);
    }

    bool TagInArray(Collider2D[] array, string tag)
    {
        foreach (var collider in array)
        {
            if (collider.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }
}
