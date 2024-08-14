using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Targeter : MonoBehaviour
{

    public List<Target> targets = new List<Target>();
    public Target currentTarget {  get; private set; }


    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target))
        {
            return;
        }
        targets.Add(target);


    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
            return;
        }
        targets.Remove(target);
    }
    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }
        currentTarget = targets[0];
        return true;

    }

    public void CancleLockon()
    {
        currentTarget = null;
    }
}
