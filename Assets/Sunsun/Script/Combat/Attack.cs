using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack 
{
    [field: SerializeField]
   public string AnimationName { get; private  set; }

    [field: SerializeField]
    public float TransitionDuration { get; private set; }

    [field: SerializeField]
    public int ComboStateIndex { get; private set; } = -1;

    [field: SerializeField]
    public float ComboAttackTime { get; private set; } = -1;

    [field: SerializeField]
    public float ForceTime { get; private set; } = -1;

    [field: SerializeField]
    public float Force { get; private set; } = -1;

    [field: SerializeField]
    public int Damage{ get; private set; } = -1;
    //À»°h¶ZÂ÷
    [field: SerializeField]
    public float knockbackRange { get; private set; } = 5f;
}
