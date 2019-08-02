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
        selection.GetComponent<SelectionPlayerGui>().initial();
        NetworkServer.RegisterHandler(MsgTypes.playerPrefabSelect,OnResponse);
        base.OnStartServer();
    }

    private void OnConnectedToServer()
    {
        
        selection.SetActive(true);
        selection.GetComponent<SelectionPlayerGui>().enabled = true;
        selection.GetComponent<SelectionPlayerGui>().initial();
        throw new NotImplementedException();
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
}
