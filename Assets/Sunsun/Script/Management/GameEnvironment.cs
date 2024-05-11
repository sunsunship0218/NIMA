using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnvironment : MonoBehaviour
{
    private static GameEnvironment instance;
    private List<GameObject> checkpoints = new List<GameObject>();
    public List<GameObject> Checkpoints { get { return checkpoints; } }
    public static GameEnvironment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnvironment();
                instance.Checkpoints.AddRange(
                    GameObject.FindGameObjectsWithTag("Checkpoints")
                    );
            }
            return instance;
           
        }
    }
}