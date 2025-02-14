using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveExitScript : MonoBehaviour
{
    [SerializeField] private Object nextscene;
    string sceneName;
    public GameObject outline;
    BoxCollider2D checkCollider;

    bool canChangeScene;

    private void Start()
    {
        sceneName = AssetDatabase.GetAssetPath(nextscene);
        sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName);
        checkCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)checkCollider.offset, checkCollider.size, 0);

        canChangeScene = TagInArray(colliders, "Player");

        outline.SetActive(canChangeScene);

        if(Input.GetKeyDown(KeyCode.E) && canChangeScene)
        {
            SceneLoader.Instance.LoadNextScene(sceneName);
        }
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
