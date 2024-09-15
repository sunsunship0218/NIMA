
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
        //-----------------------------------------------------------------------------------
        Target closeTarget = null;
        float closetTargetDistance = Mathf.Infinity;
        //尋找最近目標
        foreach (Target target in targets)
        {            
           //超出螢幕範圍,持續找目標直到找到
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);
            if( viewPos.x<0 || viewPos.x>1 || viewPos.y<0 || viewPos.y>1) { continue; }

            //螢幕範圍正中間
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
    //當target目標物被摧毀時,還存在,才呼叫此方法,進行垃圾回收

    // 當某個 Collider 進入觸發區域時，如果它有 Target 組件，將該目標加入 targets 列表中
    // 並訂閱其 onDestroyed 事件，當目標被摧毀時自動呼叫 RemoveTarget 方法
    void RemoveTarget(Target target)
    {
        if (currentTarget == target)
        {
            cineTargetGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
        }
     //當target物件ondestoryed摧毀時,取消訂閱remove方法,並把鎖定對象從List移除
        target.onDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
