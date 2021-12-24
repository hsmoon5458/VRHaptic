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

public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public GameObject LobbyUIResearcher, LobbyUIParticipant, LobbyUIPCView;
    public GameObject connectButtonResearcher, connectButtonParticipant, connectButtonPCView;
    public static int userType; //1 is researcher, 2 is participant, and 3 is PCview

    private void Start()
    {
        if (PhotonNetwork.IsConnected) // if it is connected to server, do nothing
        {
            connectButtonResearcher.SetActive(false);
            connectButtonParticipant.SetActive(false);
            connectButtonPCView.SetActive(false);

            if (userType == 1)
            {
                LobbyUIResearcher.SetActive(true);
            }
            else if (userType == 2)
            {
                LobbyUIParticipant.SetActive(true);
            }
            else
            {
                LobbyUIPCView.SetActive(true);
            }
        }
        else //otherwise, creat Nickname to connect to server
        {
        }

    }
    public void ClickedResearcher()
    {
        userType = 1;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "Researcher";
        Debug.Log("Try Connect to Server...");
    }

    public void ClickedParticipant()
    {
        userType = 2;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "Participant";
        Debug.Log("Try Connect to Server...");
    }
    
    public void ClickedPCBypassLogin()
    {
        userType = 3;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "PCView";
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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //do nothing
        }
        else
        {
            PhotonNetwork.LoadLevel(0); // go back to lobby
        }


        //in case go back to lobby from the room
        try
        { 
            if(userType == 1)
            {
                LobbyUIResearcher.SetActive(true);
            }
            else if (userType == 2)
            {
                LobbyUIParticipant.SetActive(true);
            }
            else
            {
                LobbyUIPCView.SetActive(true);
            }

        }
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
