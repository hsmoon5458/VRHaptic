 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
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
    public TMP_InputField nickNameInputField;
    public TMP_InputField nickNameInputFieldPCSide; //this is for PC

    private void Start()
    {
        if (PhotonNetwork.IsConnected) // if it is connected to server, do nothing
        {

        }
        else //otherwise, creat Nickname to connect to server
        {
            string randStr = Random.Range(0, 9999).ToString();
            nickNameInputField.text = randStr;

            try { nickNameInputFieldPCSide.text = randStr; } //in case PCSide UI is disabled.
            catch { }
        }

    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = nickNameInputField.text.ToString();
        PhotonNetwork.NickName = nickNameInputFieldPCSide.text.ToString();

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
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            //do nothing
        }
        else
        {
            PhotonNetwork.LoadLevel(0); // go back to lobby
        }
        

        //in case go back to lobby from the room
        try { LobbyUI.SetActive(true); }
        catch { }    

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
