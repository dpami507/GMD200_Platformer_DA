using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireWall : PlayerInteractable
{
    [Header("Lever")]
    public Transform leverPos;
    Animator leverAnimator;
    BoxCollider2D leverCollider;
    public GameObject leverOutline;
    bool canFlipLever;
    bool leverFlipped;

    [Header("Flame Thrower")]
    public GameObject flameThrower;
    public SpriteRenderer arrowIndicator;
    public Color activeColor;
    public Color inactiveColor;

    void Start()
    {
        leverFlipped = false;
        leverAnimator = leverPos.GetComponent<Animator>();
        leverCollider = leverPos.GetComponent<BoxCollider2D>();
        flameThrower.SetActive(true);  
        arrowIndicator.color = activeColor;
    }

    private void Update()
    {
        Collider2D[] colliders = GetCollidersInBox((Vector2)leverPos.position + leverCollider.offset, leverCollider.size);

        canFlipLever = TagInArray(colliders, "Player");

        if (leverFlipped == false)
            leverOutline.SetActive(canFlipLever);

        if (Input.GetKeyDown(KeyCode.E) && canFlipLever)
        {
            FlipLeverOff();
        }
    }

    public void FlipLeverOff() //thats mean
    {
        leverAnimator.SetTrigger("Flip");
        flameThrower.SetActive(false);
        leverOutline.SetActive(false);
        leverFlipped = true;
        arrowIndicator.color = inactiveColor;
    }
}
