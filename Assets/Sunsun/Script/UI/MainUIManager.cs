using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Button NewGameBtn;
    [SerializeField] Button ExitBtn;
    [SerializeField] Button DevInfoBtn;
    [SerializeField] Button LoadBtn;
    [SerializeField] AudioSource audioSource;
    int animatorHashIn;
    int animatorHashOut;

    private void Awake()
    {
        NewGameBtn.onClick.AddListener(StartGame);
        ExitBtn.onClick.AddListener(OnApplicationQuit);
      //  DevInfoBtn.onClick.AddListener(ShowDevInfo);
     //   LoadBtn.onClick.AddListener(LoadGame);
    }
    private void Start()
    {
        animatorHashIn = Animator.StringToHash("Crossfade_Start");
        animatorHashOut = Animator.StringToHash("Crossfade_End");
    }
    void StartGame()
    {
        StartCoroutine(LoadScene());
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }
    IEnumerator LoadScene()
    {
        animator.CrossFadeInFixedTime(animatorHashIn, 0.14f, 0);
        audioSource.Play();
        float waitTime = Mathf.Max(1f, audioSource.clip.length);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }
}
