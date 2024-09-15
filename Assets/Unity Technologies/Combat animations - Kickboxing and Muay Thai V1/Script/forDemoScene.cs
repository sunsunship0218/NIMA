using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class forDemoScene : MonoBehaviour
{

    private Animator animator;
    private AnimationClip currentState;
    private float timechange;

    public float timeChangeAnimation = 5;
    public int animationNumber=0;
    public AnimationClip[] animationsClip;
    public TextMesh text;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeAnimationState(animationsClip[animationNumber]);
        timechange = timeChangeAnimation;
    }

    void ChangeAnimationState(AnimationClip newState)
    {
        if (currentState == newState) return;

        AnimationClipSettings tSettings = AnimationUtility.GetAnimationClipSettings(newState);
        tSettings.loopTime = true;
        AnimationUtility.SetAnimationClipSettings(newState, tSettings);

        text.text = newState.name;

        animator.Play(newState.name);

        currentState = newState;
    }

    // Update is called once per frame
    void Update()
    {
        if (timechange > 0)
        {
            timechange -= Time.deltaTime;
        }
        else
        {
            if (animationNumber<animationsClip.Length-1)
            {
                animationNumber++;
            }
            else
            {
                animationNumber = 0;
            }

            ChangeAnimationState(animationsClip[animationNumber]);

            timechange = timeChangeAnimation;

        }

        
    }
}
