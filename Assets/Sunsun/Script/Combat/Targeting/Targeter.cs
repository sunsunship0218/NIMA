
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{

    public List<Target> targets = new List<Target>();
    public Target currentTarget {  get; private set; }
    [SerializeField] CinemachineTargetGroup cineTargetGroup;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }


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
        //-----------------------------------------------------------------------------------
        Target closeTarget = null;
        float closetTargetDistance = Mathf.Infinity;
        //�M��̪�ؼ�
        foreach (Target target in targets)
        {            
           //�W�X�ù��d��,�����ؼЪ�����
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);
            if( viewPos.x<0 || viewPos.x>1 || viewPos.y<0 || viewPos.y>1) { continue; }

            //�ù��d�򥿤���
            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if (toCenter.sqrMagnitude < closetTargetDistance)
            {
                closeTarget= target;
                closetTargetDistance= toCenter.sqrMagnitude;
            }
        }

        if (closeTarget == null) { return false; }
        currentTarget = closeTarget;
        //-----------------------------------------------------------------------------------
        cineTargetGroup.AddMember(currentTarget.transform, 1f ,2f);
        return true;

    }

    public void CancleLockon()
    {
        if (currentTarget == null) return;
        cineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }
    //��target�ؼЪ��Q�R����,�٦s�b,�~�I�s����k,�i��U���^��

    // ��Y�� Collider �i�JĲ�o�ϰ�ɡA�p�G���� Target �ե�A�N�ӥؼХ[�J targets �C��
    // �íq�\�� onDestroyed �ƥ�A��ؼгQ�R���ɦ۰ʩI�s RemoveTarget ��k
    void RemoveTarget(Target target)
    {
        if (currentTarget == target)
        {
            cineTargetGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
        }
     //��target����ondestoryed�R����,�����q�\remove��k,�ç���w��H�qList����
        target.onDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
