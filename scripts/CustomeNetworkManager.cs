using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Networking;
public class MsgTypes
{
    public const short playerPrefabSelect = MsgType.Highest+1;
    
    public class PlayerPrefabMsg : MessageBase
    {
        public short controllerID;
        public short prefabIndex;
    }
}
public class CustomeNetworkManager : NetworkManager
{
    public short playerIndex;
    [SerializeField] private GameObject selection;
    public GameObject getPrefab()
    {
        return playerPrefab;
    }

    public void changedIndex()
    {
        
    }
    public override void OnStartServer()
    {
        selection.SetActive(true);
        selection.GetComponent<SelectionPlayerGui>().enabled = true;
        print("hello i am SERVER");
        GameObject.FindWithTag("Manager").GetComponent<SelectorPLayer>().setUISelcetion(true);
        NetworkServer.RegisterHandler(MsgTypes.playerPrefabSelect,OnResponse);
        base.OnStartServer();
        
    }
    
    private void OnConnectedToServer()
    {
        
        selection.SetActive(true);
        selection.GetComponent<SelectionPlayerGui>().enabled = true;
        print("hello i am CLIENT");
       // GameObject.FindWithTag("Manager").GetComponent<SelectorPLayer>().setUISelcetion(true);
    }


    public override void OnStartClient(NetworkClient client)
    {
        GameObject.FindWithTag("Manager").GetComponent<SelectorPLayer>().setUISelcetion(true);
        base.OnStartClient(client);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
    }

    public void OnResponse(NetworkMessage networkMessage)
    {
        MsgTypes.PlayerPrefabMsg msg = networkMessage.ReadMessage<MsgTypes.PlayerPrefabMsg>();
        playerPrefab = spawnPrefabs[msg.prefabIndex];
        base.OnServerAddPlayer(networkMessage.conn,msg.controllerID);
      //  selection.GetComponent<SelectionPlayerGui>().initial();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        client.RegisterHandler(MsgTypes.playerPrefabSelect, OnRequest);
        base.OnClientConnect(conn);
    }

    public void OnRequest(NetworkMessage networkMessage)
    {
        MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
        msg.controllerID = networkMessage.ReadMessage<MsgTypes.PlayerPrefabMsg>().controllerID;
        msg.prefabIndex = playerIndex;
        client.Send(MsgTypes.playerPrefabSelect,msg);
    }
    
    
    
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
        msg.controllerID = playerControllerId; 
        NetworkServer.SendToClient(conn.connectionId,MsgTypes.playerPrefabSelect,msg);
        //base.OnServerAddPlayer(conn, playerControllerId);
    }
    public void switchChar(CustomeNetworkBehavior old, int index)
    {
        
        GameObject gameObject = Instantiate(spawnPrefabs[index],old.gameObject.transform.position,old.gameObject.transform.rotation);
        playerPrefab = spawnPrefabs[index];
        Destroy(old.gameObject);
      //  OnServerAddPlayer(conn,gameObject.GetComponent<PlayerController>().playerControllerId);
        NetworkServer.ReplacePlayerForConnection(old.connectionToClient, gameObject, 0);
        
    }
    public void switchChar2(CustomeNetworkBehavior old, string name)
    {
        GameObject gameObject =null;
        int index = 1;
        for (int i = 1; i < spawnPrefabs.Count; i++)
        {
            if(spawnPrefabs[i].name.Equals(name))
            {
                index = i;
                gameObject = Instantiate(spawnPrefabs[i],old.gameObject.transform.position,old.gameObject.transform.rotation);
            }
        }
        playerPrefab = spawnPrefabs[index];
        Destroy(old.gameObject);
        //  OnServerAddPlayer(conn,gameObject.GetComponent<PlayerController>().playerControllerId);
        if(gameObject != null)
              NetworkServer.ReplacePlayerForConnection(old.connectionToClient, gameObject, 0);
    }


}
