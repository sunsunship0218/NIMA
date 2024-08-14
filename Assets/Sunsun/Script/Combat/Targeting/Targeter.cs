using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{

    public List<Target> targets = new List<Target>();
    public Target currentTarget {  get; private set; }
    [SerializeField] CinemachineTargetGroup cineTargetGroup;

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target))
        {
            return;
        }
        targets.Add(target);
        //移除鎖定物,如果被摧毀的話
        target.onDestroyed += RemoveTarget;

    }

    void OnTriggerExit(Collider other)
    {
        //沒有偵測到鎖定目標的話return
        if (!other.TryGetComponent<Target>(out Target target))
        {
            return;
        }
        RemoveTarget(target);
    }

  

    public bool SelectTarget()
    {
        //沒有所選目標物不執行
        if (targets.Count == 0) { return false; }
        Debug.Log(currentTarget);
        currentTarget = targets[0];
        cineTargetGroup.AddMember(currentTarget.transform, 1f ,2f);
        return true;

    }

    public void CancleLockon()
    {
        if (currentTarget == null) return;
        cineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }
    //only be called when targets get destroyed and exist
    void RemoveTarget(Target target)
    {
        if (currentTarget == target)
        {
            cineTargetGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
            target.onDestroyed -= RemoveTarget;
            targets.Remove(target);
        }
    }
}
