using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> onDestroyed;

    //��ؼгQ�R����Ĳ�o,�R���ۤv(�U���^��),�w���M�z�����b
    // ��ؼгQ�R����Ĳ�o�ƥ� (OnDestroy �O Unity ���ͩR�g����k�A����Q�R���ɷ|�۰ʩI�s)
    // this �N���e�Q�R���� Target ����A�ñN�Ӫ���ǻ����Ҧ��q�\�̡A�q�`�O Targeter �ӳB�z�C
    void OnDestroy()
    {
        // this�O�ثe����Q�R�����ؼЪ�,�q��(�Ҧp Targeter) �ؼФw�g�Q�R��
        onDestroyed?.Invoke(this);
    }

}
