 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string name;
    public int sceneIndex;
    public int maxPlayer;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public GameObject LobbyUI;
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect to Server...");
    }
  
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server");
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");
        LobbyUI.SetActive(true);
    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];
        PhotonNetwork.LoadLevel(roomSettings.sceneIndex);
        //Room option
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = (byte)roomSettings.maxPlayer,
            IsVisible = true,
            IsOpen = true,
            PublishUserId = true
        };

        PhotonNetwork.JoinOrCreateRoom(roomSettings.name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player entered the room");
        base.OnPlayerEnteredRoom(newPlayer);

    }
}
