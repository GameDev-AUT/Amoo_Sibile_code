﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{
    // Start is called before the first frame update

    private CharacterController characterController;
    private Animator animator;
    private AnimationState AnimationState = new AnimationState("speed",0,false);
    private AnimationHandler animationHandler;
    private bool attack = false;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        animationHandler = this.GetComponent<AnimationHandler>();
    }

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizantal = Input.GetAxis("Horizontal");
        
        var movment = this.transform.forward * vertical;
        characterController.SimpleMove(movment*3);
        this.transform.Rotate(new Vector3(0,horizantal*4,0));

        AnimationState.Speed = vertical;
        AnimationState.AnimationName = "speed";
        AnimationState.TrigerType = false;
        animationHandler.setAnimation(AnimationState);
        
        if (Input.GetKeyDown(KeyCode.E) && !attack)
        {
            attack = true;
            Invoke("attacked", 1f);
            AnimationState.AnimationName = "attack";
            AnimationState.TrigerType = true;
            animationHandler.setAnimation(AnimationState);
        } 
    }
    private void attacked()
    {
        //AnimationState.TrigerType = false;
        attack = false;
    }
}