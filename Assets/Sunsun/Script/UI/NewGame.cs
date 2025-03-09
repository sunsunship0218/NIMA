using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    [SerializeField] Animator animator;
    int animatorHashIn;
    int animatorHashOut;

    private void Start()
    {
        animatorHashIn = Animator.StringToHash("Crossfade_Start");
        animatorHashOut = Animator.StringToHash("Crossfade_End");
    }
    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        animator.CrossFadeInFixedTime(animatorHashIn, 0.14f, 0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
 
}
