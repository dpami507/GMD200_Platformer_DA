using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    public Collider2D[] GetCollidersInBox(Vector2 pos, Vector2 size)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos, size, 0);
        return colliders;
    }

    public bool TagInArray(Collider2D[] array, string tag)
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
