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
    public GameObject roomSelectionUIResearcher, roomSelectionUIParticipant, roomSelectionUIPCView, interactionTypeUIResearcher, interactionTypeUIParticipant;
    public GameObject connectButtonResearcher, connectButtonParticipant, connectButtonPCView;
    public static int userType, interactionType; //1 is researcher, 2 is participant, and 3 is PCview for userType, and 1 for controller and 2 for hand tracking
    public Material screenFadeMaterial;

    private void Start()
    {
        if (PhotonNetwork.IsConnected) // if it is connected to server, do nothing
        {
            connectButtonResearcher.SetActive(false);
            connectButtonParticipant.SetActive(false);
            connectButtonPCView.SetActive(false);
            Debug.Log("IS CONNECTED");
            //once they came back to lobby from the room, they will reset interaction type and enter the room.
            interactionType = 0;
            if(userType == 1)
            {
                roomSelectionUIResearcher.SetActive(false);
                interactionTypeUIResearcher.SetActive(true);
            }
            if(userType == 2)
            {
                roomSelectionUIParticipant.SetActive(false);
                interactionTypeUIParticipant.SetActive(true);
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

    public void ClickedController()
    {
        interactionType = 1;
        StartCoroutine(ActiveRoomSelectionPanel());
    }

    public void ClickedHandTracking()
    {
        interactionType = 2;
        StartCoroutine(ActiveRoomSelectionPanel());
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
        Debug.Log("Joined the Lobby");
        //in case go back to lobby from the room
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //do nothing
        }
        else
        {
            PhotonNetwork.LoadLevel(0); // go back to lobby
        }
                
        try
        { 
            if(userType == 1)
            {
                interactionTypeUIResearcher.SetActive(true);
            }
            else if (userType == 2)
            {
                interactionTypeUIParticipant.SetActive(true);
            }
            else
            {
                roomSelectionUIPCView.SetActive(true);
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

    IEnumerator ActiveRoomSelectionPanel()
    {
        yield return new WaitForSeconds(1f);
        if (userType == 1) //researcher
        {
            roomSelectionUIResearcher.SetActive(true);
        }
        else if (userType == 2) //participant
        {
            roomSelectionUIParticipant.SetActive(true);
        }
        else
        {
            roomSelectionUIPCView.SetActive(true);
        }

    }

    IEnumerator FadeOut()
    {
        float time = Time.time;
        int alpha = 0;
        while (alpha < 255)
        {            
            alpha = Mathf.FloorToInt(Mathf.Lerp(0, 255, Mathf.InverseLerp(time, time + 1.5f, Time.time)));
            screenFadeMaterial.color = new Color(0f, 0f, 0f, alpha / 255f);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float time = Time.time;
        int alpha = 255;
        while (alpha > 0)
        {
            alpha = Mathf.FloorToInt(Mathf.Lerp(255, 0, Mathf.InverseLerp(time, time + 2.2f, Time.time)));
            screenFadeMaterial.color = new Color(0f, 0f, 0f, alpha / 255f);
            yield return null;
        }
    }

}
