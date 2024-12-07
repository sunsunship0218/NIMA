using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColiHandler : MonoBehaviour
{
    [SerializeField] GameObject BlockColi;

    public void EnableBlockColi()
    {

        BlockColi.SetActive(true);
        
    }
    public void DisableBlockColi()
    {

        BlockColi.SetActive(false);

    }
}
