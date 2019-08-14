using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectionPlayerGui : MonoBehaviour
{
    private bool selected = false;
    //[SerializeField] private GameObject player;
    [SerializeField]private GameObject customeNetworkManager;
    // Start is called before the first frame update
    public void initial()
    {
       // Invoke("defaultSelection",10);
       // player = customeNetworkManager.GetComponent<CustomeNetworkManager>().getPrefab();
      //  print("safdasfa");
      GameObject.FindWithTag("local").GetComponent<movment>().enabled = false;
      GameObject.FindWithTag("local").GetComponent<Player>().enabled = false;
      //  player.GetComponent<movment>().enabled = false;
    }

    public  void vikingSelect()
    {
        @select(2);
    }
    public  void knightSelect()
    {
        @select(1);
    }
    public  void witchSelect()
    {
        @select(6);
    }
    public  void sceletonSelect()
    {
        @select(4);
    }
    public  void valkirySelect()
    {
        @select(5);
    }
    public  void wizardSelect()
    {
        @select(3);
    }
    public  void select(short index)
    {
        Debug.Log("wtf");
        customeNetworkManager.GetComponent<CustomeNetworkManager>().playerIndex = index;
        GameObject.FindWithTag("local").GetComponent<CustomeNetworkBehavior>().changeChar(index);
    //    customeNetworkManager.GetComponent<CustomeNetworkManager>().CmdSwitchChar(Movment.gameObject);
  //      customeNetworkManager.GetComponent<CustomeNetworkManager>().RpcSwitchChar(Movment.gameObject);
        selected = true;
    }

    public void select(string name)
    {
        Debug.Log(name);
        GameObject.FindWithTag("local").GetComponent<CustomeNetworkBehavior>().changeChar(name);
        selected = true;  
    }

    


    public void defaultSelection()
    {
        if (!selected)
            customeNetworkManager.GetComponent<CustomeNetworkManager>().playerIndex = 1;
      //  player.GetComponent<movment>().enabled = true;
    }
}
