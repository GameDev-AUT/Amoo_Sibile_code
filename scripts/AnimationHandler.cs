using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;

    private CustomeNetworkBehavior customeNetworkBehavior;
    // Start is called before the first frame update
    void Start()
    {
        customeNetworkBehavior = this.GetComponent<CustomeNetworkBehavior>();
        animator = GetComponentInChildren<Animator>();
    }

    public void setAnimation(AnimationState animationState)
    {
        if (animationState.TrigerType)
        {
            
            animator.SetTrigger(animationState.AnimationName);
        }
        else if(!animationState.AsABool)
        {
            animator.SetFloat(animationState.AnimationName,animationState.Speed);
        }
        else
        {
            animator.SetBool(animationState.AnimationName,true);
        }
        customeNetworkBehavior.CmdAnimationToServer(animationState.AnimationName,animationState.Speed,animationState.TrigerType,animationState.AsABool);
    }
    
    public void updateAnimation(AnimationState animationState)
    {
        if (animationState.TrigerType)
        {
            
            animator.SetTrigger(animationState.AnimationName);
        }
        else if(!animationState.AsABool)
        {
            animator.SetFloat(animationState.AnimationName,animationState.Speed);
        }
        else
        {
            animator.SetBool(animationState.AnimationName,true);
        }
    }   
    
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
