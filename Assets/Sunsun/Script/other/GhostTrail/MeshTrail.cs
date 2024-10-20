using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public static MeshTrail Instance { get; private set; }
    public float activetime;
    bool istrailActive;

    void playGhostTrail()
    {
        if (!istrailActive)
        {
            istrailActive = true;
            StartCoroutine(StartActiveTrail(activetime));
        }
        IEnumerator StartActiveTrail(float ActiveDuration)
        {
            yield return null;
        }
    }
}

