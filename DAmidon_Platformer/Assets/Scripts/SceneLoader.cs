using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;
    public float transitionTime;
    public GameObject transition;

    public static SceneLoader Instance;

    private void Start()
    {
        Instance = this;

        transition.SetActive(true);
    }

    public void LoadNextScene(string scene)
    {
        StartCoroutine(LoadScene(scene));
    }

    IEnumerator LoadScene(string scene)
    {
        //Play cover
        animator.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load
        SceneManager.LoadScene(scene);

        //Wait
        yield return new WaitForSeconds(transitionTime / 2);

        //Play reveal
        animator.SetTrigger("End");
    }
}
