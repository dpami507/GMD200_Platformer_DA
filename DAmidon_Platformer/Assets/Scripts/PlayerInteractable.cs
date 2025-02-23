using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    //Get all the colliders in an OverlapBox
    public Collider2D[] GetCollidersInBox(Vector2 pos, Vector2 size)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos, size, 0);
        return colliders;
    }

    //Check if there is a tag in an array
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
