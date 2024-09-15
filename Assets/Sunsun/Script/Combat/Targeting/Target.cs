using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> onDestroyed;

    //當目標被摧毀時觸發,摧毀自己(垃圾回收),預防清理不乾淨
    // 當目標被摧毀時觸發事件 (OnDestroy 是 Unity 的生命週期方法，當物件被摧毀時會自動呼叫)
    // this 代表當前被摧毀的 Target 物件，並將該物件傳遞給所有訂閱者，通常是 Targeter 來處理。
    void OnDestroy()
    {
        // this是目前死後被摧毀的目標物,通知(例如 Targeter) 目標已經被摧毀
        onDestroyed?.Invoke(this);
    }

}
