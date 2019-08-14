using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class CustomeNetworkBehavior : NetworkBehaviour
{
    private AnimationState animationState = new AnimationState("speed",0,false);
    private AnimationHandler animationHandler;

  //  [SyncVar (hook = "OnHealthChange")]public float health;
    private EffectHandler EffectHandler;
    
    private CustomeNetworkManager CustomeNetworkManager;
    // Start is called before the first frame update
    void Start()
    {
//        health = 100;
        CustomeNetworkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<CustomeNetworkManager>();
        EffectHandler = this.GetComponent<EffectHandler>();
        animationHandler = this.GetComponent<AnimationHandler>();
        if (!isLocalPlayer)
        {
            this.gameObject.GetComponent<movment>().enabled = false;
        }
        else
        {
            this.gameObject.tag = "local";
            this.gameObject.GetComponent<movment>().enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //animation handler
    [Command]
    public void CmdAnimationToServer(string animationName,float speed,bool isTriger,bool asbool)
    {
        setAnimation(animationName,speed,isTriger,asbool); 
        RpcAnimationToClient(animationName,speed,isTriger,asbool);
    }
    [ClientRpc]
    private void RpcAnimationToClient(string animationName,float speed,bool isTriger,bool asbool)
    {
        if(!isLocalPlayer)
            setAnimation(animationName,speed,isTriger,asbool);
    }
    private void setAnimation(string animationName,float speed,bool isTriger,bool asbool)
    {
        animationState.Speed = speed;
        animationState.AnimationName = animationName;
        animationState.TrigerType = isTriger;
        animationState.AsABool = asbool;
        animationHandler.updateAnimation(animationState);
    }
    //
    
    //effect handler
    [Command]
    public void CmdEffectToServer(string ability)
    {
        setEffect(ability);
        RpcEffectToClient(ability);
    }

    [ClientRpc]
    public void RpcEffectToClient(string ability)
    {
        if(!isLocalPlayer)
             setEffect(ability);
    }   
    private void setEffect(string ability)
    {
        EffectHandler.updateEffect(this.GetComponentInChildren<Gun>().transform.position,ability);
    }
    //
    
    //player changer
    public void changeChar(int index)
    {
    //    if(isLocalPlayer)
    CmdChangeChar(index);
    }
    [Command]
    public void CmdChangeChar(int index)
    {
        Debug.Log("here");
        NetworkManager.singleton.GetComponent<CustomeNetworkManager>().switchChar(this,index);
      //  CustomeNetworkManager.switchChar(this,index);
    }

    public void changeChar(string name)
    {
        CmdChangeChar2(name);

    }
    [Command]
    public void CmdChangeChar2(string name)
    {
        Debug.Log("here");
        NetworkManager.singleton.GetComponent<CustomeNetworkManager>().switchChar2(this,name);
        //  CustomeNetworkManager.switchChar(this,index);
        
    }
    
    
    
    
    //
    [Command]
    public void CmdIsAttacking(bool attack)
    {
        updateIsAttacking(attack);
        RpcIsAttacking(attack);
    }
    
    [ClientRpc]
    public void RpcIsAttacking(bool attack)
    {
            if(!isLocalPlayer)
               updateIsAttacking(attack);
    }

    private void updateIsAttacking(bool attack)
    {
      //  if (id.GetInstanceID().Equals(GetComponent<NetworkIdentity>().GetInstanceID()))
     //   {
            print("ferestad");
            GetComponent<Player>().Attack = attack;
            // }
    }
/*
    [Command]
    public void CmdHealth(float delta)
    {
        health = delta;
        GetComponent<Player>().updateHealth(health);
    }

    public void OnHealthChange(float delta)
    {
        health = delta;
        GetComponent<Player>().updateHealth(health);
        print("plsssss");
    }
    
    */
    
}
