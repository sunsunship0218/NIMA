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
        //������w��,�p�G�Q�R������
        target.onDestroyed += RemoveTarget;

    }

    void OnTriggerExit(Collider other)
    {
        //�S����������w�ؼЪ���return
        if (!other.TryGetComponent<Target>(out Target target))
        {
            return;
        }
        RemoveTarget(target);
    }

  

    public bool SelectTarget()
    {
        //�S���ҿ�ؼЪ�������
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
