using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{
    // Start is called before the first frame update

    private CharacterController characterController;

    private bool onChoosingCharState = true;
    private AnimationState AnimationState = new AnimationState("speed",0,false);
    private AnimationHandler animationHandler;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject power;
    private EffectHandler EffectHandler;
    private bool attack = false;
     void Start()
    {
        characterController = GetComponent<CharacterController>();
        EffectHandler = GetComponentInChildren<EffectHandler>();
        animationHandler = this.GetComponent<AnimationHandler>();
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
        if (!attack)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                attack = true;
                Invoke("attacked", 1f);
                AnimationState.AnimationName = "attack";
                AnimationState.TrigerType = true;
                animationHandler.setAnimation(AnimationState);
                EffectHandler.buildEffect(GetComponentInChildren<Gun>().transform.position,"attack");
                this.GetComponent<Player>().Attack = true;
                GetComponent<CustomeNetworkBehavior>().CmdIsAttacking(true);
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                attack = true;
                Invoke("attacked", 1f);
                AnimationState.AnimationName = "skill";
                AnimationState.TrigerType = true;
                animationHandler.setAnimation(AnimationState);
                EffectHandler.buildEffect(GetComponentInChildren<Gun>().transform.position,"ability");
                this.GetComponent<Player>().Attack = true;
                GetComponent<CustomeNetworkBehavior>().CmdIsAttacking(true);
            }
        }
    }
    private void attacked()
    {
        //AnimationState.TrigerType = false;
        attack = false;
        this.GetComponent<Player>().Attack = false;
        GetComponent<CustomeNetworkBehavior>().CmdIsAttacking(false);

    }

    public bool OnChoosingCharState
    {
        get => onChoosingCharState;
        set => onChoosingCharState = value;
    }

    public bool Attack
    {
        get => attack;
        set => attack = value;
    }
}
