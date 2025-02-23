using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveExitScript : PlayerInteractable
{
    [SerializeField] private Object nextscene;
    string sceneName;
    public GameObject outline;
    BoxCollider2D checkCollider;

    bool canChangeScene;

    private void Start()
    {
        //Allows for scene to be drag and dropped into object
        sceneName = AssetDatabase.GetAssetPath(nextscene);
        sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName);

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
