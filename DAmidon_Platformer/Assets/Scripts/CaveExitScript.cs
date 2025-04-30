using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveExitScript : PlayerInteractable
{
    [SerializeField] string sceneName;
    public GameObject outline;
    BoxCollider2D checkCollider;

    bool canChangeScene;

    private void Start()
    {
        checkCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Get all colliders in area
        Collider2D[] colliders = GetCollidersInBox(transform.position + (Vector3)checkCollider.offset, checkCollider.size);

        //Check if one of the coliders is the palyer
        canChangeScene = TagInArray(colliders, "Player");

        //Set active if player is in area
        outline.SetActive(canChangeScene);

        //Get input from player
        if(Input.GetKeyDown(KeyCode.E) && canChangeScene)
        {
            //Load scene
            SceneLoader.Instance.LoadNextScene(sceneName);
        }
    }
}
