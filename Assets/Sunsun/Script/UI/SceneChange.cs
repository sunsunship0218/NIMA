using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] Button titleButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() => ReloadScene());
        titleButton.onClick.AddListener(() => GoToMain(0));
        restartButton.onClick.AddListener(() => Debug.Log("Restart Button Clicked!"));
    }
    public void GoToMain(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ReloadScene( )
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
