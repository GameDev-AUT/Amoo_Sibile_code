using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class CustomeNetworkBehavior : NetworkBehaviour
{
    private AnimationState animationState = new AnimationState("speed",0,false);
    private AnimationHandler animationHandler;

    private EffectHandler EffectHandler;

    private CustomeNetworkManager CustomeNetworkManager;
    // Start is called before the first frame update
    void Start()
    {
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
    public void CmdAnimationToServer(string animationName,float speed,bool isTriger)
    {
        setAnimation(animationName,speed,isTriger); 
        RpcAnimationToClient(animationName,speed,isTriger);
    }
    [ClientRpc]
    private void RpcAnimationToClient(string animationName,float speed,bool isTriger)
    {
        if(!isLocalPlayer)
            setAnimation(animationName,speed,isTriger);
    }
    private void setAnimation(string animationName,float speed,bool isTriger)
    {
        animationState.Speed = speed;
        animationState.AnimationName = animationName;
        animationState.TrigerType = isTriger;
        animationHandler.updateAnimation(animationState);
    }

    
    //effect handler
    [Command]
    public void CmdEffectToServer(string effectName)
    {
        setEffect(effectName);
        RpcEffectToClient(effectName);
    }

    [ClientRpc]
    public void RpcEffectToClient(string effectName)
    {
        if(!isLocalPlayer)
             setEffect(effectName);
    }   
    private void setEffect(string effectName)
    {
        EffectHandler.updateEffect(effectName,this.GetComponentInChildren<Gun>().transform.position);
    }



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
}
