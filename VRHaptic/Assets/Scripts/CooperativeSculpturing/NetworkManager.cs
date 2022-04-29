 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;



public class NetworkManager : MonoBehaviourPunCallbacks
{
 
    public override void OnJoinedLobby()
    {
        try { VibrationManager.singletone.CloseIOPort(); }
        catch { Debug.LogWarning("COM Port is not connected"); }
        PhotonNetwork.LeaveRoom();
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
