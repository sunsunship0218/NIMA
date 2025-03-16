using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExitGame : MonoBehaviour
{
    public void OnApplicationQuit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
