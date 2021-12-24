using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawn : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    private Transform spawningTransform;
    void Start()
    {

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if(PhotonNetwork.NickName == "Researcher")
        {
            spawningTransform = GameObject.Find("Player1SpawnPosition").transform;
        }
        else if (PhotonNetwork.NickName == "Participant")
        {
            spawningTransform = GameObject.Find("Player2SpawnPosition").transform;
        }
        else
        {
            spawningTransform = this.transform;
        }
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", spawningTransform.position, spawningTransform.rotation);
        spawnedPlayerPrefab.name = PhotonNetwork.NickName;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
